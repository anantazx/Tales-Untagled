using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField]private float fallDelay = 0.3f;
    [SerializeField]private float respawnDelay = 5f;


    [SerializeField] private Rigidbody2D RB;
    private Vector2 initialPosition;
    private bool isFalling;

    // Start is called before the first frame update
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    private IEnumerator FallAndRespawn()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        RB.bodyType = RigidbodyType2D.Dynamic;

        // mengulang platform dan membalikan ke posisi awal
        yield return new WaitForSeconds(respawnDelay);
        RB.bodyType = RigidbodyType2D.Static;
        transform.position = initialPosition;
        isFalling = false;
        
    }

}
