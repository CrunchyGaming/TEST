using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float playerHealth = 100f;

    public Slider healthSlider;


    public void AddHealth(float health) {

        if (playerHealth < 100) {
            playerHealth += health;
            UpdateHeatlhBar();

            // Cap the health at 100
            playerHealth = Mathf.Min(playerHealth, 100f);
        }

    }

    public void TakeDamage(float damage) {

        playerHealth -= damage;
        UpdateHeatlhBar();

        if (playerHealth <= 0) {
            GetComponent<DeathHandler>().HandleDeath();
        }

    }

    void UpdateHeatlhBar()
    {
        healthSlider.value = playerHealth;
    }
    

}
