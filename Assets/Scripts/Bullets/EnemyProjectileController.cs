using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    //moving the projectile
    [SerializeField] float projectileSpeed;
    private Vector3 playerDirection;

    //bullet health
    [SerializeField] int bulletHealth;


    void Start()
    {
        playerDirection = FindObjectOfType<PlayerController>().transform.position - transform.position;
        playerDirection.Normalize();
    }


    void Update()
    {
        //bullet movement
        transform.position += playerDirection * projectileSpeed * Time.deltaTime;      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //deal damage to player

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }

    public void DamageBullet(int damageTaken)
    {
        bulletHealth -= damageTaken;

        if (bulletHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
