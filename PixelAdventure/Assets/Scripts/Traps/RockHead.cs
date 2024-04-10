using System.Collections;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    [Header("Rock Head")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Animator anim;
    [SerializeField] private int headType;
    [SerializeField] private int dir;
    [SerializeField] private Vector2[] direction;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private bool isMoving;

    [SerializeField] private bool hit;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject player;

    void Update()
    {
        Move();
        Hit();
    }

    void Move()
    {
        anim.SetBool("Blink", isMoving);
        if (isMoving)
        {
            transform.Translate(direction[dir] * speed * Time.deltaTime);
            //rig.AddForce(direction[dir] * speed, ForceMode2D.Force);
        }
        if (Physics2D.Raycast(transform.position, direction[dir], distance, ground))
        {
            isMoving = false;
            Animations();
            Direction();
            StartCoroutine(Delay());
        }
    }

    void Hit()
    {
        if (player != null)
        {
            if (player.transform.position.y - 0.5 > transform.position.y && dir == 0)
                hit = true;

            else if (player.transform.position.x - 0.5 > transform.position.x && dir == 1)
                hit = true;

            else if (player.transform.position.y + 0.5f < transform.position.y && dir == 2)
                hit = true;

            else if (player.transform.position.x + 0.5f < transform.position.x && dir == 3)
                hit = true;
            else
                hit = false;
        }

        if (player != null && Physics2D.Raycast(transform.position, direction[dir], distance * 1.5f, ground))
        {
            if (hit) player.GetComponent<PlayerController>().Hit(200, direction[dir]);
        }
    }

    void Direction()
    {
        if (headType == 0)
        {
            if (dir == 0) dir = 2;
            else if (dir == 2) dir = 0;

        }
        else if (headType == 1)
        {
            if (dir == 1) dir = 3;
            else if (dir == 3) dir = 1;

        }
        else if (headType == 2)
        {
            if (dir == 0) dir = 1;
            else if (dir == 1) dir = 2;
            else if (dir == 2) dir = 3;
            else if (dir == 3) dir = 0;
        }
        else if (headType == 3)
        {
            if (dir == 0) dir = 3;
            else if (dir == 3) dir = 2;
            else if (dir == 2) dir = 1;
            else if (dir == 1) dir = 0;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
    }

    void Animations()
    {
        switch (dir)
        {
            case 0:
                anim.SetTrigger("Top");
                break;
            case 1:
                anim.SetTrigger("Right");
                break;
            case 2:
                anim.SetTrigger("Botton");
                break;
            case 3:
                anim.SetTrigger("Left");
                break;

        }
    }

}
