using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerDetectionRange;
    private Vector3 directionToMove;

    private Transform playerToChase;

    private bool isChasing = false;

    private Animator enemyAnimator;
    [SerializeField] GameObject bloodSplatter;

    [SerializeField] int enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
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

        if(directionToMove != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }

        if(playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        if(enemyHealth <= 0)
        {
            Instantiate(bloodSplatter, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }

}
