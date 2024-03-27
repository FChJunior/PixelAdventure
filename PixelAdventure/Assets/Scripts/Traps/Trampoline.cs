using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline")]
    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Jump");
            other.gameObject.GetComponent<PlayerController>().Jumping(500f);
        }
    }
}
