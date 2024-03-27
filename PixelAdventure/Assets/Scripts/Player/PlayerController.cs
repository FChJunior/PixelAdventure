using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Input Script")]
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private bool inControl;

    #region Colisões
    [Header("Colisões")]
    [SerializeField] private bool inGround;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform foot;
    [SerializeField] private bool hit;
    public bool _hit {get { return hit; }}
    #endregion

    #region Player Movimentação
    [Header("Player Movimentação")]
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Vector2 move;
    [SerializeField] private float speed;
    [SerializeField] private bool isMoving;

    public bool _isMoving { get { return isMoving; } }
    public bool _inGround { get { return inGround; } }
    #endregion

    #region Player Pulo
    [Header("Player Pulo")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool inputJump;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool enableDoubleJump;
    [SerializeField] private float nJump;
    [SerializeField] private bool isFall;
    [SerializeField] private bool isRising;

    public bool _isJumpig { get { return isJumping; } }
    public float _nJump { get { return nJump; } }
    public bool _isFall { get { return isFall; } }

    #endregion

    #region Player Dash
    [Header("Player Dash")]
    [SerializeField] private bool inputDash;
    [SerializeField] private bool enableDash;
    [SerializeField] private bool inDash;
    [SerializeField] private Vector2 directionDash;
    [SerializeField] private float timeDash;
    [SerializeField] private float dashForce;
    public bool _inDash { get { return inDash; } }
    #endregion

    #region Player Wall Slide
    [Header("Player Wall Slide")]
    [SerializeField] private bool inWall;
    [SerializeField] private bool wallSliding;
    [SerializeField] private float distanceWall;
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float timerWallJump;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private LayerMask wall;
    public bool _wallSliding { get { return wallSliding; } }
    #endregion

    #region Métodos Iniciais
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        player = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        inControl = true;
        inputJump = false;
        isJumping = false;
        nJump = 0f;
        enableDoubleJump = true;
        enableDash = true;
    }
    private void Update()
    {
        Inputs();
        CheckGround();
    }
    private void FixedUpdate()
    {
        if (inDash) return;

        if (inControl)
        {
            Movement();
            Jump();
            Dash();
            WallSliding();
        }
    }
    #endregion

    private void Inputs()
    {
        move.x = playerInputs.Movement() * speed;
        move.y = player.velocity.y;

        if (playerInputs.Jump()) inputJump = true;
        if (playerInputs.Dash() && enableDash) inputDash = true;

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }
    private void Movement()
    {
        player.velocity = move;
        isMoving = player.velocity.x != 0;
        if (!inWall && !hit) Flip();
    }
    private void Flip()
    {
        if (player.velocity.x > 0) transform.eulerAngles = Vector2.zero;
        else if (player.velocity.x < 0) transform.eulerAngles = Vector2.up * 180;
    }
    private void Jump()
    {
        if (inputJump)
        {
            isJumping = true;

            if (!wallSliding)
            {
                if (nJump == 0 || (nJump == 0 && !enableDoubleJump))
                {
                    nJump++;
                    player.velocity = Vector2.zero;
                    player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (nJump == 1 && enableDoubleJump)
                {
                    nJump++;
                    player.velocity = Vector2.zero;
                    player.AddForce(Vector2.up * jumpForce * 1.5f, ForceMode2D.Impulse);
                }
            }
            else
            {
                nJump = 2;
                StartCoroutine(StopInput(timerWallJump));
                player.velocity = Vector2.zero;
                player.AddForce(-transform.right * wallJumpForce + transform.up * wallJumpForce * 1.5f, ForceMode2D.Impulse);
                transform.eulerAngles += Vector3.up * 180;
            }

            inputJump = false;
        }
    }
    public void Jumping(float jumpForce)
    {
        nJump = 1;
        player.velocity = Vector2.zero;
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public void Jumping(float jumpForce, Vector2 dir)
    {
        nJump = 1;
        player.velocity = Vector2.zero;
        StartCoroutine(StopInputs());
        player.AddForce(dir * jumpForce, ForceMode2D.Impulse);
    }

    public void Hit(float jumpForce, Vector2 dir)
    {
        player.velocity = Vector2.zero;
        hit = true;
        StartCoroutine(StopInputs());
        player.AddForce(dir * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(StopInput(0.25f));
    }

    IEnumerator StopInputs()
    {
        float g = player.gravityScale;
        player.gravityScale = 0f;
        inControl = false;
        yield return new WaitForSeconds(timerWallJump);
        inControl = true;
        player.gravityScale = g;
    }

    private void CheckGround()
    {
        inGround = Physics2D.OverlapCircle(foot.position, 0.1f, ground);
        isFall = player.velocity.y < 0 && !inGround;
        isRising = player.velocity.y > 0 && !inGround;
        if (inGround && !isRising && !isFall)
        {
            isJumping = false;
            nJump = 0;
            enableDash = true;
        }

    }
    private void Dash()
    {
        if (inputDash && enableDash)
        {
            inputDash = false;
            enableDash = false;
            directionDash.x = playerInputs.Movement() != 0 || playerInputs.DirectionDash() != 0 ? playerInputs.Movement() : transform.right.x;
            directionDash.y = playerInputs.DirectionDash();
            StartCoroutine(InDash());
        }
    }
    IEnumerator InDash()
    {
        inDash = true;
        float g = player.gravityScale;
        player.gravityScale = 0f;
        inControl = false;

        player.velocity = directionDash * dashForce;
        yield return new WaitForSeconds(timeDash);

        player.gravityScale = g;
        inDash = false;
        inControl = true;
    }
    private void WallSliding()
    {
        inWall = Physics2D.Raycast(transform.position, transform.right, distanceWall, wall);
        wallSliding = inWall && !inGround && player.velocity.y < 0;

        if (wallSliding && player.velocity.y < wallSlidingSpeed)
        {
            player.velocity = new Vector2(player.velocity.x, wallSlidingSpeed);
        }
    }
    IEnumerator StopInput(float time)
    {
        inControl = false;
        yield return new WaitForSeconds(time);
        inControl = true;
        hit = false;
    }
}
