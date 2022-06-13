using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healAmount;
    private bool wasPickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player")  && !wasPickedUp)
        {
            wasPickedUp = true;

            collision.GetComponent<PlayerHealthHandler>().HealPlayer(healAmount);
            
            Destroy(gameObject);
        }

    }
}
