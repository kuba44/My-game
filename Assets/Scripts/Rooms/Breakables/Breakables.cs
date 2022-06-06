using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    private Animator breakableAnimator;

    [SerializeField] GameObject[] brokenPiece;

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
        foreach ( GameObject piece in brokenPiece)
        {
            float rotationRand = Random.Range( -30f, 30f);

            Instantiate(
                piece,
                transform.position,
                Quaternion.Euler( 0, 0, rotationRand )
                );
        }

        Destroy ( gameObject );
    }
}
