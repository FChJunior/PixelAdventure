using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
    [SerializeField] float minY, maxY;
    
    void LateUpdate()
    {
        transform.position = player.position;
        if(transform.position.x < minX) transform.position = new Vector3(minX, transform.position.y);
        if(transform.position.x > maxX) transform.position = new Vector3(maxX, transform.position.y);

        if(transform.position.y < minY) transform.position = new Vector3(transform.position.x, minY);
        if(transform.position.y > maxY) transform.position = new Vector3(transform.position.x, maxY);
        
    }
}
