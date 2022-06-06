using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerMovement
    [SerializeField] float movementSpeed;  
    private Vector2 movementInput;
    private float currentMovementSpeed;

    //PlayerDash
    private bool canDash = true;
    [SerializeField] float dashSpeed, 
                           dashLength, 
                           dashCooldown;

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
    private bool canShoot = true;

    //objects
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Transform weaponArm;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;


    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();

        currentMovementSpeed = movementSpeed;
    }


    void Update()
    {
        PlayerMovement();

        PlayerGunAiming();

        PlayerAnimations();

        PlayerShooting();

        PlayerDash();
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            playerAnimator.SetTrigger("Dash");

            StartCoroutine(DashCooldown());
            StartCoroutine(DashLength());
        }
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
        if (!canShoot) return;

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

        playerRigidbody.velocity = movementInput * currentMovementSpeed;
    }

    private IEnumerator DashLength()
    {
        currentMovementSpeed = dashSpeed;

        canShoot = false;

        yield return new WaitForSeconds(dashLength);

        currentMovementSpeed = movementSpeed;

        canShoot = true;
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    public bool IsDashing()
    {
        if( currentMovementSpeed == dashSpeed )
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
