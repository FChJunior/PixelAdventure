using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D col2D;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && transform.position.y < other.gameObject.transform.position.y)
        {
            StartCoroutine(Enable());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 dir;
            if(transform.position.x > other.transform.position.x)
                dir = new Vector2(-0.5f,1);
            else if(transform.position.x < other.transform.position.x)
                dir = new Vector2(0.5f,1);
            else
                dir = new Vector2(0,1);
            other.GetComponent<PlayerController>().Hit(150, new Vector2(dir.normalized.x * 3, dir.normalized.y));
        }
    }

    IEnumerator Enable()
    {
        anim.SetBool("On", true);
        yield return new WaitForSeconds(0.6875f);
        col2D.enabled = true;
        yield return new WaitForSeconds(2);
        col2D.enabled = false;
        anim.SetBool("On", false);
    }
}
