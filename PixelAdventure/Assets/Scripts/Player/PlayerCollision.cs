using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Player Collision")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform parent;
    [SerializeField] private float speed, dashForce;

    void Start()
    {
        speed = player._speed;
        dashForce = player._dashForce;
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
        if (other.gameObject.tag == "Sand")
        {
            player._speed = 2.5f;
            player._dashForce = 5;
        }
        if (other.gameObject.tag == "Ice")
        {
            player._inIce = true;
        }
        if (other.gameObject.tag == "Mud")
        {
            player._speed = 0;
            player._enabledDash = false;
        }
        if (other.tag == "Platform")
        {
            transform.SetParent(other.GetComponent<Platform>()._follow);
            if (other.GetComponent<Platform>()._typePlatform == 1) other.GetComponent<Platform>()._right = !other.GetComponent<Platform>()._right;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HeadBlock")
        {
            transform.SetParent(parent);
        }
        if (other.gameObject.tag == "Sand")
        {
            player._speed = speed;
            player._dashForce = dashForce;
        }
        if (other.gameObject.tag == "Ice")
        {
            player._inIce = false;
        }
        if (other.gameObject.tag == "Mud")
        {
            player._speed = speed;
            player._enabledDash = true;
        }
        if (other.tag == "Platform")
        {
            transform.SetParent(parent);
            if (other.GetComponent<Platform>()._typePlatform == 1) other.GetComponent<Platform>()._right = !other.GetComponent<Platform>()._right;
        }
    }
}
