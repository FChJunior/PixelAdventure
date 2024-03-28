using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D col2D;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2[] dirJump;
    [SerializeField] private Vector3[] dir;
    [SerializeField] private int direction;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();

    }

    void Start()
    {
        col2D.enabled = true;
    }
    void Update()
    {
        transform.eulerAngles = dir[direction];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("Hit", true);
            other.gameObject.GetComponent<PlayerController>().Jumping(280, dirJump[direction]);
            col2D.enabled = false;
            StartCoroutine(Disable());
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        anim.SetBool("Hit", false);
        yield return new WaitForSeconds(2f);
        spriteRenderer.enabled = true;
        col2D.enabled = true;
    }
}
