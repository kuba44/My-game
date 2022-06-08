using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.Instance.healthSlider.maxValue = maxHealth;

        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer( int damageAmount)
    {
        currentHealth -= damageAmount;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            UIManager.Instance.DeathScreenOn();
            
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthUI()
    {        
        UIManager.Instance.healthSlider.value = currentHealth;

        UIManager.Instance.healthText.text = currentHealth + " / " + maxHealth;
    }

}
