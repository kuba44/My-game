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

    public void SpawnBrokenPieces()
    {
        foreach ( GameObject piece in brokenPiece)
        {
            float positionRandX = transform.position.x + Random.Range( -0.5f, 0.5f );
            float positionRandY = transform.position.y + Random.Range( -0.25f, 0.25f );
            float rotationRand = Random.Range( -30, 30);

            Instantiate ( piece, new Vector3 ( positionRandX, positionRandY, 0 ), Quaternion.Euler ( new Vector3 ( 0, 0, rotationRand ) ) );
        }
    }

    public void Destroy()
    {
        Destroy ( gameObject );
    }
}
