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
    public void FireHit(GameObject other)
    {
        Vector2 dir = Vector2.zero;
            if(transform.position.x > other.transform.position.x)
                dir.x = -0.5f;
            else if(transform.position.x < other.transform.position.x)
                dir.x = 0.5f;
            else
                dir.x = 0f;

            if(transform.position.y > other.transform.position.y)
                dir.y = -0.5f;
            else if(transform.position.y <= other.transform.position.y)
                dir.y = 1f;

            other.gameObject.GetComponent<PlayerController>().Hit(200, dir);
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
