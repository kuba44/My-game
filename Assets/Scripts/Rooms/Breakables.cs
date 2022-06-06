using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    private Animator breakableAnimator;

    private void Start()
    {
        breakableAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player") )
        {
            bool isDashing = collision.gameObject.GetComponent<PlayerController>().IsDashing();

            if (isDashing)
            {
                breakableAnimator.SetTrigger("Break");
            }
        }
        else if ( collision.CompareTag("Player bullet") )
        {
            breakableAnimator.SetTrigger("Break");
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
