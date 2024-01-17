using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float playerHealth = 100f;



    public void AddHealth(float health) {

        if (playerHealth < 100) {
            playerHealth += health;

            // Cap the health at 100
            playerHealth = Mathf.Min(playerHealth, 100f);
        }

    }

    public void TakeDamage(float damage) {

        playerHealth -= damage;

        if (playerHealth <= 0) {
            GetComponent<DeathHandler>().HandleDeath();
        }

    }
}
