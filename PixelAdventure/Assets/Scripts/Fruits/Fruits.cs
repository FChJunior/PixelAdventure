using System.Collections;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [Header("Atibutos")]
    [SerializeField] private Animator anim;
    // [SerializeField] private string typeFruit;
    // public string _typeFruit { get { return typeFruit; } }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            anim.SetTrigger("Hit");
            StartCoroutine(Disable());
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
}
