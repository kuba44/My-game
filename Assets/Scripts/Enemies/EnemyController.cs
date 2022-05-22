using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float enemySpeed;
    [SerializeField] Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    private Vector3 directionToMove;

    private Transform playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            Debug.Log("player is chase range");
        }   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
    }

}
