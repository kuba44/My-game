using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PlayerMovement
    [SerializeField] float movementSpeed;  
    private Vector2 movementInput;
    private float currentMovementSpeed;

    //PlayerGunAiming
    private Camera mainCamera;

    //PlayerDash
    private bool canDash = true;
    [SerializeField] float dashSpeed, 
                           dashLength, 
                           dashCooldown;
    private bool canShoot = true;

    //PlayerAnimations
    private Animator playerAnimator;

    //PlayerGuns
    [SerializeField] List<WeaponsController> avalibleGuns = new List<WeaponsController>();
    private int currentGun;

    //objects
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Transform weaponArm;

    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();

        currentMovementSpeed = movementSpeed;

        for (int i = 0; i < avalibleGuns.Count; i++)
        {
            if ( avalibleGuns[i].gameObject.activeInHierarchy )
            {
                currentGun = i;
            }
        }

        SetWeaponUI();
    }


    void Update()
    {
        PlayerGunSwitching();

        PlayerGunAiming();

        PlayerMovement();

        PlayerAnimations();

        PlayerDash();
    }

    private void PlayerGunSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (avalibleGuns.Count > 0)
            {
                currentGun++;

                if (currentGun >= avalibleGuns.Count)
                {
                    currentGun = 0;
                }

                foreach (WeaponsController weapon in avalibleGuns)
                {
                    weapon.gameObject.SetActive(false);
                }

                avalibleGuns[currentGun].gameObject.SetActive(true);
                SetWeaponUI();

            }
        }
    }

    private void SetWeaponUI()
    {
        UIManager.Instance.WeaponChangeUI(
            avalibleGuns[currentGun].GetGunImage(),
            avalibleGuns[currentGun].GetGunName()
            );
    }

    private void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            playerAnimator.SetTrigger("Dash");

            StartCoroutine( DashCooldown() );
            StartCoroutine( DashLength() );
            StartCoroutine( GetComponent<PlayerHealthHandler>().PlayerFlash( 4 ) );
            StartCoroutine( GetComponent<PlayerHealthHandler>().InvincibilityTime( dashLength + 0.25f ) );
        }
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

    private void PlayerMovement()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();

        playerRigidbody.velocity = movementInput * currentMovementSpeed;
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

    public bool CanPlayerShoot()
    {
        return canShoot;
    }

    public List<WeaponsController> GetAvalibleGuns()
    {
        return avalibleGuns;
    }

    public Transform GetWeaponsArm()
    {
        return weaponArm;
    }

}
