using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 7.5f;
    private Rigidbody2D bulletRigidbody;
    [SerializeField] GameObject bulletEffect;

    [SerializeField] int damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(bulletEffect, gameObject.transform.position, gameObject.transform.rotation);

        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);
        }

        Destroy(gameObject);
    }
}
