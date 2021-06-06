using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public HealthBar tankHealthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        tankHealthBar.SetMaxHealth(maxHealth);
        tankHealthBar.SetHealth(currentHealth);
    }

    public void TankHurt(int damage)
    {
        currentHealth -= damage;
        tankHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TankHeal()
    {
        currentHealth = maxHealth;
        tankHealthBar.SetHealth(currentHealth);
    }
}
