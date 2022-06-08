using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;

    [SerializeField] float invincibilityTime;
    private bool isInvincible;

    [SerializeField] SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.Instance.healthSlider.maxValue = maxHealth;

        UpdateHealthUI();

        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer( int damageAmount)
    {
        if (isInvincible) return;

        currentHealth -= damageAmount;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            UIManager.Instance.DeathScreenOn();
            
            gameObject.SetActive(false);
        }

        StartCoroutine( InvincibilityTime( invincibilityTime ) );
        StartCoroutine( PlayerFlash( 7 ) );

    }

    private void UpdateHealthUI()
    {        
        UIManager.Instance.healthSlider.value = currentHealth;

        UIManager.Instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    public IEnumerator PlayerFlash( int numberOfFlashes )
    {
        for( int i = 0; i < numberOfFlashes; i++ )
        {
            playerSprite.color = new Color( playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.1f );

            yield return new WaitForSeconds( .1f );

            playerSprite.color = new Color (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f );

            yield return new WaitForSeconds( .1f );
        }
    }

    public IEnumerator InvincibilityTime( float invincibleTime )
    {
        isInvincible = true;

        yield return new WaitForSeconds( invincibleTime );
        
        isInvincible = false;
    }

}
