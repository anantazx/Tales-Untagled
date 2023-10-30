using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject CurrentOneWayPlatform;

    [SerializeField] private BoxCollider2D PlayerCollider;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (CurrentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision()); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            CurrentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            CurrentOneWayPlatform = null;
        }
        
    }

    private IEnumerator DisableCollision()
    {
        CompositeCollider2D PlatfromCollider = CurrentOneWayPlatform.GetComponent<CompositeCollider2D>();

        Physics2D.IgnoreCollision(PlayerCollider, PlatfromCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(PlayerCollider, PlatfromCollider, false);
    }

}
