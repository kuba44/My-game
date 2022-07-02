using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickup : MonoBehaviour
{
    [SerializeField] WeaponsController weapon;
    private bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pickedUp)
        {
            bool gunOnPlayer = false;

            foreach (WeaponsController weaponToCheck in collision.GetComponent<PlayerController>().GetAvalibleGuns())
            {
                if ( weapon.GetGunName() == weaponToCheck.GetGunName() )
                {
                    gunOnPlayer = true;
                }
                
            }

            if (!gunOnPlayer)
            {
                Instantiate(weapon, collision.GetComponent<PlayerController>().GetWeaponsArm());

                pickedUp = true;

                Destroy(gameObject);
            }
        }
    }

}
