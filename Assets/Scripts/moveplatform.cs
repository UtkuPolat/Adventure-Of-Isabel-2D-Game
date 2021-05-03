using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplatform : MonoBehaviour
{

    public Transform firstpos, secondpos;
    public float speed;

    Vector3 nextpos;

    private void Start()
    {
        nextpos = firstpos.position;
    }

    private void Update()
    {
        if(transform.position== firstpos.position)
        {
            nextpos = secondpos.position;

        }

        if (transform.position == secondpos.position)
        {
            nextpos = firstpos.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(firstpos.position, secondpos.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }

        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
