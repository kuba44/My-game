using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    [SerializeField] Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerDetectionRange;
    private Vector3 directionToMove;

    private Transform playerToChase;

    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, playerToChase.position) < playerDetectionRange)
        {
            isChasing = true;
        }

        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position;
        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
        }

        directionToMove.Normalize();
        enemyRigidbody.velocity = directionToMove * enemySpeed;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }

}
