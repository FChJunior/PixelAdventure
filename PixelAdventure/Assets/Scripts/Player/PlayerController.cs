using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Componentes e Scripts Auxiliares

    [Header("Componentes e Scripts Auxiliares")]
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask wall;
    #endregion

    #region Atributos de Controle
    //=============================================================================//
    //==================== Atributos de Controle de fluxo de jogo. ================//
    //=============================================================================//
    [Header("Atributos de Controle")]
    [SerializeField] private bool inControl; // Verifica se o player está no controle.
    [SerializeField] private bool hit; // Verifica se o player sofreu hit.
    [SerializeField] private bool life; // Verifica se o player sofreu hit.
    [SerializeField] private bool dead; // Verifica se o player morreu.
    [SerializeField] private bool enabledMove; // Verifica se o player pode se mover.
    [SerializeField] private bool enabledJump; // Verifica se o player pode pular.
    [SerializeField] private bool enabledDoubleJump; // Verifica se o player pode pular duas vezes.
    [SerializeField] private bool enabledDash; // Verifica se o player pode usar o dash.
    [SerializeField] private bool enabledWallSlide; // Verifica se o player pode usar o wall slide.
    //=============================================================================//
    //=========== Atributos GETERS e SETERS de Controle de fluxo de jogo. =========//
    //=============================================================================//
    public bool _inControl { get { return inControl; } set { inControl = value; } } // Possibilita que outros scripts consigam acessar e modificar se o jogador está controlando o personagem.
    public bool _hit { get { return hit; } } // Possibilita que o script de animação consiga acessar se o jogador sofreu hit.
    public bool _life { get { return life; } }  // Possibilita que outros scripts consigam acessar se a vida do player.
    public bool _dead { get { return dead; } } // Possibilita que outros scripts consigam acessar se a vida do está morto.
    public bool _enabledMove { set { enabledMove = value; } } // Possibilita que outros scripts consigam habilitar ou desabilitar o movimento do Player.
    public bool _enabledJump { set { enabledJump = value; } } // Possibilita que outros scripts consigam habilitar ou desabilitar o pulo do Player.
    public bool _enabledDoubleJump { set { enabledDoubleJump = value; } } // Possibilita que outros scripts consigam habilitar ou duplo pulo o movimento do Player.
    public bool _enabledDash { set { enabledDash = value; } } // Possibilita que outros scripts consigam habilitar ou desabilitar o dash do Player.
    public bool _enabledWallSlide { set { enabledWallSlide = value; } } // Possibilita que outros scripts consigam habilitar ou wall slide o movimento do Player.
    //=============================================================================//
    #endregion

    #region Atribrutos de Colisão com o Chão
    //=============================================================================//
    //===================== Atribrutos de Colisão com o Chão. =====================//
    //=============================================================================//
    [Header("Atribrutos de Colisão com o Chão")]
    [SerializeField] private bool inGround; // Verifica se o Player está no chão.
    [SerializeField] private bool inIce; // Verifica se o Player está no chão.
    [SerializeField] private bool isFalling; // Verifica se o Player está caindo.
    [SerializeField] private bool isRising; // Verifica se o Player está subindo.
    //=============================================================================//
    //============== Atributos GETERS e SETERS de Colisão com o Chão. =============//
    //=============================================================================//
    public bool _inGround { get { return inGround; } set { inGround = value; } } // Possibilita que outros scripts consigam acessar e modificar se o player está se no chão.
    public bool _inIce { get { return inIce; } set { inIce = value; } }
    public bool _isFalling { get { return isFalling; } set { isFalling = value; } } //Possibilita que outros scripts consigam acessar se o player está caindo.
    public bool _isRising { get { return isRising; } set { isRising = value; } } //Possibilita que outros scripts consigam acessar se o player está subindo.
    //=============================================================================//
    #endregion

    #region Atribrutos Player Movimentação
    //=============================================================================//
    //============== Atributos de Controle de movimentação do Player. =============//
    //=============================================================================//
    [Header("Player Movimentação")]
    [SerializeField] private Vector2 inputMove; // Detentem os valores que receberá do script Player Inputs.
    [SerializeField] private float speed; // Recebe a velociada do player.
    [SerializeField] private bool isMoving; // Verifica se o Player está se movendo.
    //=============================================================================//
    //============ Atributos GETERS e SETERS de movimentação do Player. ===========//
    //=============================================================================//
    public bool _isMoving { get { return isMoving; } } // Possibilita que outros scripts consigam acessar se o player está se movendo.
    public float _speed { get { return speed; } set { speed = value; } }
    //=============================================================================//
    #endregion

    #region Atribrutos Player Pulo
    //=============================================================================//
    //=================== Atributos de Controle de pulo do Player. ================//
    //=============================================================================//
    [Header("Player Pulo")]
    [SerializeField] private bool inputJump; // Recebe a entrada que virá do scripe Player Input para pular.
    [SerializeField] private float jumpForce; // Controle de Força do Pulo do Player.
    [SerializeField] private bool isJumping; // Verifica se o player está pulando.
    [SerializeField] private float nJump; // Verifica em qual pulo o Player está (1 == primeiro pulo e 2 == segundo pulo)
    //=============================================================================//
    //================= Atributos GETERS e SETERS de pulo do Player. ==============//
    //=============================================================================//
    public bool _isJumping { get { return isJumping; } set { isJumping = value; } } // Permite que outros Scripts possam acessar se o Player está pulando.
    public float _nJump { get { return nJump; } set { nJump = value; } } // Permite que outros Scripts possam acessar se em qual pulo o Player está.
    //=============================================================================//
    #endregion

    #region Atribrutos Player Wall Slide
    //=============================================================================//
    //=============== Atributos de Controle de Wall Slide do Player. ==============//
    //=============================================================================//
    [Header("Player Wall Slide")]
    [SerializeField] private bool inWall; // Verifica se o Player está na Parede.
    [SerializeField] private float distanceWall; // A distancia minima do Player para a Parede para atividar o Wall Slide.
    [SerializeField] private bool wallSliding; // Verifica se o Player está deslizando na Parede.
    [SerializeField] private float wallSlidingSpeed; // Velocidade de slide do Player.
    [SerializeField] private float timerWallJump; // Timer para desativar os controles para o player executar o pulo na Parede.
    [SerializeField] private float wallJumpForce; // Força de pulo na Parede.
    //=============================================================================//
    //============= Atributos GETERS e SETERS de Wall Slide do Player. ============//
    //=============================================================================//
    public bool _wallSliding { get { return wallSliding; } } // Possibilita que outros Scripts possam acessar se o Player está deslizando na parede.
                                                             //=============================================================================//
    #endregion

    #region Atribrutos Player Dash
    //=============================================================================//
    //=============== Atributos de Controle de Dash do Player. ==============//
    //=============================================================================//
    [Header("Player Dash Slide")]
    [SerializeField] private bool inputDash; // Recebe o input do jogador pra poder usar o Dash.
    [SerializeField] private bool inDash; // Verifica se o Player está em Dash;
    [SerializeField] private bool canDash; // Verifica se o Player pode usar o Dash;
    [SerializeField] private Vector2 directionDash; // Verifica em qual direção o Dash irá;
    [SerializeField] private float dashForce; // Força que o player será impulsionado pelo Dash.
    [SerializeField] private float timeDash; // Delay do Dash, para seabilitar os controle do jogador.
    //=============================================================================//
    //============= Atributos GETERS e SETERS de Dash do Player. ============//
    //=============================================================================//
    public bool _inDash { get { return inDash; } } // Possibilita que outros Scripts possam acessar se o Player está usando o Dash.
    public float _dashForce { get { return dashForce; } set { dashForce = value; } }
    
    //=============================================================================//
    #endregion

    #region Métodos de Inicialização
    //=========================================================================//
    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        player = GetComponent<Rigidbody2D>();
    }
    //=========================================================================//
    private void Start()
    {
        inputJump = false;
        isJumping = false;
        inDash = false;
        wallSliding = false;
        nJump = 0f;
    }
    //=========================================================================//
    #endregion

    #region Inputs Player
    //=========================================================================//
    //============== Métodos para atuliazar os inputs do Jogador ==============//
    //======================== e atulização de variaves =======================//
    //=========================================================================//
    private void Update()
    {
        Inputs(); // Chama o método que lê os inputs do jogador que vem do script Player Inputs.
    }
    private void Inputs()
    {
        if (!inIce) { inputMove.x = playerInputs.Movement() * speed; }
        else { inputMove.x = playerInputs.MovementIce() * speed/3f; }// Recebe o valor de input de movimento horizontal do jogador e multiplica pela velociadade para dar movimento ao player.
        inputMove.y = player.velocity.y; // Recebe o valor da propria velociadade de Y do player para não interferir na gravidae do jogo.

        if (playerInputs.Jump() && enabledJump) inputJump = true; // Chama o pulo do Player.
        if (playerInputs.Dash() && enabledDash) inputDash = true; // Chama o Dash do PLayer.

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0); // Tirar depois.
    }
    //=========================================================================//
    #endregion

    #region Mecânicas do Player

    #region Método Principal
    //=========================================================================//
    //============== Métodos para atuliazar a Física do Player ================//
    //=========================================================================//
    private void FixedUpdate()
    {
        if (inDash) return; // Verifica se o Player está executando o Dash para tirar o controle temporariamente do jogador.

        if (inControl) // Verifica se o jogador pode controlar.
        {
            if (enabledMove) Movement(); // Chama a movimentação e o Flip do Player.
            if (enabledJump) Jump(); // Chama os pulos do player.
            if (enabledDash) Dash(); // Chama o Dash do Player.
            if (enabledWallSlide) WallSliding(); // Chama o Wall Slide do Player
        }
    }
    //=========================================================================//
    #endregion

    #region Movimentação do Player
    //=========================================================================//
    //============== Método para movimentação e o Flip do Player ==============//
    //=========================================================================//
    private void Movement()
    {
        if(!inIce)player.velocity = inputMove; // Atuliza a velocidade do Player via Rigidbody.
        else player.AddForce(inputMove, ForceMode2D.Impulse);
        
        isMoving = player.velocity.x != 0 && inputMove.x != 0; // Atuliza a varivel se player está se movendo.
        if (!inWall && !hit) Flip(); // Chama o método para flipar o player no eixo Y caso necessario.

    }
    private void Flip()
    {
        if (player.velocity.x > 0) transform.eulerAngles = Vector2.zero; // Se o PLayer estiver se movendo para a DIREITA.
        else if (player.velocity.x < 0) transform.eulerAngles = Vector2.up * 180;  // Se o PLayer estiver se movendo para a ESQUERDA.}
    }
    //=========================================================================//
    #endregion

    #region Pulos do Player
    //=========================================================================//
    //======================= Método de Pulos do Player =======================//
    //=========================================================================//
    private void Jump()
    {
        if (transform.parent.tag != "HeadBlock")
        {
            isFalling = player.velocity.y < 0;
            isRising = player.velocity.y > 0;
        }
        inGround = Physics2D.Raycast(foot.position, Vector2.down, 0.55f, wall);

        if (inputJump)
        {
            isJumping = true;

            if (!wallSliding)
            {
                if (nJump == 0 && inGround)
                {
                    nJump++;
                    player.velocity = Vector2.zero;
                    player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (nJump == 1 && enabledDoubleJump)
                {
                    nJump++;
                    player.velocity = Vector2.zero;
                    player.AddForce(Vector2.up * jumpForce * 1.25f, ForceMode2D.Impulse);
                }
            }
            else
            {
                nJump = 1;
                StartCoroutine(StopInputs(timerWallJump));
                player.velocity = Vector2.zero;
                player.AddForce(-transform.right * wallJumpForce + transform.up * wallJumpForce * 1.5f, ForceMode2D.Impulse);
                transform.eulerAngles += Vector3.up * 180;
            }

            inputJump = false;
        }
    }
    //=========================================================================//
    public void Jumping(float jumpForce)
    {
        nJump = 1;
        isJumping = true;
        player.velocity = Vector2.zero;
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    //=========================================================================//
    public void Jumping(float jumpForce, Vector2 dir)
    {
        nJump = 1;
        isJumping = true;
        player.velocity = Vector2.zero;
        StartCoroutine(StopInputs());
        player.AddForce(dir * jumpForce, ForceMode2D.Impulse);
    }
    //=========================================================================//
    public void Hit(float jumpForce, Vector2 dir)
    {
        player.velocity = Vector2.zero;
        hit = true;
        player.AddForce(dir * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(StopInputs(0.25f));
    }
    //=========================================================================//
    #endregion

    #region Dash
    //=========================================================================//
    //======================= Método de Dash do Player ========================//
    //=========================================================================//
    private void Dash()
    {
        if (inGround) canDash = true;
        if (!canDash) inputDash = false;
        if (inputDash && canDash)
        {
            inputDash = false;
            canDash = false;
            directionDash.x = playerInputs.Movement() != 0 || playerInputs.DirectionDash() != 0 ? playerInputs.Movement() : transform.right.x;
            directionDash.y = playerInputs.DirectionDash();
            StartCoroutine(InDash());
        }
    }
    //=========================================================================//
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
    //=========================================================================//
    #endregion

    #region WallSliding
    //=========================================================================//
    //==================== Método de Wall Slide do Player =====================//
    //=========================================================================//
    private void WallSliding()
    {
        inWall = Physics2D.Raycast(transform.position, transform.right, distanceWall, wall);
        wallSliding = inWall && !inGround && player.velocity.y < 0;

        if (wallSliding && player.velocity.y < wallSlidingSpeed)
        {
            player.velocity = new Vector2(player.velocity.x, wallSlidingSpeed);
        }
    }
    //=========================================================================//
    #endregion

    #endregion

    #region Courotinas Auxiliares
    //=========================================================================//
    IEnumerator StopInputs(float time)
    {
        inControl = false;
        yield return new WaitForSeconds(time);
        inControl = true;
        isJumping = true;
        hit = false;
    }
    //=========================================================================//
    IEnumerator StopInputs()
    {
        float g = 3;
        player.gravityScale = 0f;
        inControl = false;
        yield return new WaitForSeconds(timerWallJump);
        inControl = true;
        player.gravityScale = g;
    }
    //=========================================================================//
    #endregion
}