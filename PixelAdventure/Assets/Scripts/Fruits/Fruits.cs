using System.Collections;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [Header("Atibutos")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D col2D;
    [SerializeField] private Rigidbody2D fruit;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        fruit = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            anim.SetTrigger("Hit");
            StartCoroutine(Disable());
        }
    }

    private void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            anim.SetTrigger("Hit");
            fruit.bodyType = RigidbodyType2D.Static;
            col2D.isTrigger = true;
            StartCoroutine(Disable());
            Destroy(gameObject, 0.6f);
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
}
