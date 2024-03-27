using System.Collections;
using UnityEngine;

public class Fun : MonoBehaviour
{
    [Header("Fun")]
    [SerializeField] private float timer;
    [SerializeField] private float time;
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AreaEffector2D areaEffector2D;
    [SerializeField] private Collider2D col2D;

    void Start()
    {
        particles.Play();
        anim.SetBool("On", true);
        time = 0;
        areaEffector2D.enabled = true;
        col2D.enabled = true;
    }
    void Update()
    {
        if (time >= timer)
        {
            StartCoroutine(Disable());
        }
        time += Time.deltaTime;
    }

    IEnumerator Disable()
    {
        particles.Stop();
        anim.SetBool("On", false);
        areaEffector2D.enabled = false;
        col2D.enabled = false;
        yield return new WaitForSeconds(timer);
        particles.Play();
        time = 0;
        anim.SetBool("On", true);
        areaEffector2D.enabled = true;
        col2D.enabled = true;
    }
}
