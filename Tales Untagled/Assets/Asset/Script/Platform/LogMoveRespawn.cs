using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMoveRespawn : MonoBehaviour
{
    [SerializeField] private float respawn;
    [SerializeField] private Transform respawnPoint;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        Debug.Log("Log Respawn");
        yield return new WaitForSeconds(respawn);
        transform.position = respawnPoint.position;
    }
}
