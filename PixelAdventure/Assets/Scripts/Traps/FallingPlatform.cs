using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Falling Platform")]
    [SerializeField] private Rigidbody2D anchor;
    [SerializeField] private Collider2D col2D, trigger;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Transform anchorTrans, target;

    bool returnig;
    void Update()
    {
        if (returnig)
        {
            anchorTrans.position = Vector2.MoveTowards(anchorTrans.position, target.position, 0.1f);
            transform.position = Vector2.MoveTowards(transform.position, target.position, 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.75f);
        anim.SetBool("On", false);
        particles.Stop();

        yield return new WaitForSeconds(0.5f);
        col2D.enabled = false;
        trigger.enabled = false;
        anchor.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(2f);
        anim.SetBool("On", true);
        anchor.bodyType = RigidbodyType2D.Static;
        particles.Play();
        returnig = true;

        yield return new WaitForSeconds(1f);
        col2D.enabled = true;
        trigger.enabled = true;
        returnig = false;



    }


}
