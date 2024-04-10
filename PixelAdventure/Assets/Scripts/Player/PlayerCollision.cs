using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Player Collision")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform parent;

    private void OnCollisionEnter2D(Collision2D other)
    {

    }
    private void OnCollisionExit2D(Collision2D other)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            player._isJumping = false;
            player._isFalling = false;
            player._isRising = false;
            player._nJump = 0;
        }
        if (other.tag == "Box")
        {
            float dir = transform.position.y > other.transform.position.y ? 1 : -1;
            other.gameObject.GetComponent<Box>().Hit(player, dir);
        }
        if (other.tag == "Blocks")
        {
            float dir = transform.position.y > other.transform.position.y ? 1 : -1;
            other.gameObject.GetComponent<Blocks>().Hit(player, dir);
        }
        if (other.tag == "FallingPlatform" && transform.position.y > other.transform.position.y)
        {
            other.GetComponent<FallingPlatform>().Hit();
        }
        if (other.tag == "Fire")
        {
            other.GetComponentInParent<Fire>().FireHit(gameObject);
        }
        if (other.tag == "HeadBlock")
        {
            player._isJumping = false;
            player._isFalling = false;
            player._isRising = false;
            player._nJump = 0;
            transform.SetParent(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HeadBlock")
        {
            transform.SetParent(parent);
        }
    }
}
