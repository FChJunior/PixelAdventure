using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector2 dir = Vector2.zero;
            if (transform.position.x > other.transform.position.x)
                dir.x = -0.5f;
            else if (transform.position.x < other.transform.position.x)
                dir.x = 0.5f;
            else
                dir.x = 0f;

            if (transform.position.y > other.transform.position.y)
                dir.y = -0.5f;
            else if (transform.position.y <= other.transform.position.y)
                dir.y = 1f;

            other.gameObject.GetComponent<PlayerController>().Hit(200, dir);    
        }
    }
}
