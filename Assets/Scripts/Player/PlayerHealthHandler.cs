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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer( int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
