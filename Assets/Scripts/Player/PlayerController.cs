using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerMovement
    [SerializeField] float movementSpeed;  
    private Vector2 movementInput;

    //PlayerAnimations
    private Camera mainCamera;
    private Animator playerAnimator;

    //PlayerShooting
    [SerializeField] float timeBetweenAutomaticShots;
    private float automaticShotCounter = 0;
    [SerializeField] float timeBetweenNormalShots;
    private float normalShotCounter = 0;
    private float temps;
    private bool click;

    //objects
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Transform weaponArm;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;


    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        PlayerMovement();

        PlayerGunAiming();

        PlayerAnimations();

        PlayerShooting();
    }

    private void PlayerGunAiming()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponArm.transform.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponArm.transform.localScale = Vector3.one;
        }

        weaponArm.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void PlayerAnimations()
    {
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    private void PlayerShooting()
    {
        normalShotCounter -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            temps = Time.time;
            click = true;
        }

        if (click && (Time.time - temps) > 0.2)
        {
            automaticShotCounter -= Time.deltaTime;

            if (automaticShotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                automaticShotCounter = timeBetweenAutomaticShots;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            click = false;

            if ((Time.time - temps) < 0.2)
            {
                if (normalShotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    normalShotCounter = timeBetweenNormalShots;
                }
            }
        }
    }

    private void PlayerMovement()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();

        playerRigidbody.velocity = movementInput * movementSpeed;
    }
}
