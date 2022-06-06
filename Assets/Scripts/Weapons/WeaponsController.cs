using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    //GunShooting
    [SerializeField] float timeBetweenShots;
    private float shotCounter = 0;

    //Gun types
    [SerializeField] bool isShotgun;

    //objects
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    

    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    
    void Update()
    {
        GunShooting();

        if ( Input.GetKey(KeyCode.Tab) )
        {
            gameObject.SetActive(true);
        }

    }

    private void GunShooting()
    {
        if ( !GetComponentInParent<PlayerController>().CanPlayerShoot() ) return;

        shotCounter -= Time.deltaTime;

        if ( shotCounter <= 0 && Input.GetMouseButton(0) )
        {
            if ( !isShotgun )
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
            else
            {
                for( int i = -2; i < 3; i++)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation * Quaternion.Euler ( firePoint.rotation.x, firePoint.rotation.y, i * 2.5f ) );
                }

            }

            shotCounter = timeBetweenShots;
        }

    }

}
