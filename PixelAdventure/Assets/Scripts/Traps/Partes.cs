using System.Collections;
using UnityEngine;

public class Partes : MonoBehaviour
{
    [SerializeField] private Collider2D col2D;
    [SerializeField] private PointEffector2D pointEffector2D;


    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        pointEffector2D = GetComponent<PointEffector2D>();
        
    }
    private void OnEnable() {
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.25f);
        col2D.usedByEffector = false;
        pointEffector2D.enabled = false;
    }
}