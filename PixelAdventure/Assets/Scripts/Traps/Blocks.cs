using System.Collections;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [Header("Blocks")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject blocks;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D col2D;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        GameObject player = collision2D.gameObject;
        if (player.tag == "Player")
        {
            if (collision2D.transform.position.y - 0.4f > transform.position.y)
            {
                anim.SetTrigger("Hit");
                collision2D.gameObject.GetComponent<PlayerController>().Jumping(250f);
                StartCoroutine(Disable());
            }
            else if (collision2D.transform.position.y < transform.position.y)
            {

            }
        }

    }
    public void Hit(PlayerController player, float dir)
    {
        anim.SetTrigger("Hit");
        player.GetComponent<PlayerController>().Jumping(250f, Vector2.down);
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.enabled = false;
        blocks.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        col2D.enabled = false;
        yield return new WaitForSeconds(2f);
        blocks.SetActive(false);

        float time = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(time);
            blocks.SetActive(true);
            yield return new WaitForSeconds(time);
            blocks.SetActive(false);
            time -= 0.01f;
        }
    }
}
