using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //moving the bullet
    [SerializeField] float bulletSpeed = 7.5f;
    private Rigidbody2D bulletRigidbody;

    //damaging the enemies
    [SerializeField] int damageAmount;

    //objects
    [SerializeField] GameObject bulletEffect;
    [SerializeField] GameObject[] damageEffect;


    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>(); 
    }


    void Update()
    {
        bulletRigidbody.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            int rand = Random.Range(0, damageEffect.Length);

            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);

            Instantiate(damageEffect[rand], gameObject.transform.position, gameObject.transform.rotation);
        }
        else if(collision.CompareTag("Enemy bullet"))
        {
            collision.GetComponent<EnemyProjectileController>().DamageBullet(damageAmount);
        }
        else
        {
            Instantiate(bulletEffect, gameObject.transform.position, gameObject.transform.rotation);
        }

        Destroy(gameObject);
    }
}
