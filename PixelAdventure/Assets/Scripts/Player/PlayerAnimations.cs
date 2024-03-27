using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Animações")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        anim.SetBool("Dash", player._inDash);
        if (player._inGround)
        {
            anim.SetBool("inGround", true);
            if(!player._isMoving) anim.SetFloat("Anim", 0f);
            else anim.SetFloat("Anim", 1f);
        }
        else
        {
            anim.SetBool("inGround", false);
            if(player._isJumpig && !player._isFall)
            {
                if(player._nJump == 1) anim.SetFloat("Jumps", 1f);
                else if(player._nJump == 2)anim.SetFloat("Jumps", 2f);
            }
            else if (player._isFall && !player._wallSliding) anim.SetFloat("Jumps", 0f);
            else if (player._isFall && player._wallSliding) anim.SetFloat("Jumps", 3f);
        }
        

    }
}
