using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damageAmount;

    private Collider2D player;

    private bool shouldDamagePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("spikesPop", true);
            player = collision;
            shouldDamagePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("spikesPop", false);
            shouldDamagePlayer = false;
        }
    }
    public void damagePlayer()
    {
        if ( shouldDamagePlayer)
        {
            player.GetComponent<PlayerHealthHandler>().DamagePlayer(damageAmount);
        }
    }

}
