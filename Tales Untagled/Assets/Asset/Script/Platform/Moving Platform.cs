using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int StartingPoint;
    [SerializeField] private Transform[] Points;
    [SerializeField] private float slowEffect;
    

    private int i;
    void Start()
    {
        transform.position = Points[StartingPoint].position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Points[i].position) < 0.02f)
        {
            i++;
            if (i == Points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, Points[i].position, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            rb.interpolation = RigidbodyInterpolation2D.None;
            rb.drag += slowEffect;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.drag -= slowEffect;

        }
    }

}
