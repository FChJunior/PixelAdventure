using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D col2D;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        col2D = GetComponent<Collider2D>();
    }

    void Start()
    {
        col2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("Hit");
            other.gameObject.GetComponent<PlayerController>().Jumping(400);
            col2D.enabled = false;
            StartCoroutine(Disable());
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
