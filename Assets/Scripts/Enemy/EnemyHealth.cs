using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float enemyHealth = 100f;

    public Slider healthSlider;
  

    public void EnemyTakeDamage(float damage)
    {
        enemyHealth -= damage;
        UpdateHeatlhBar();

        if (enemyHealth <= 0) {
            Die();
        }

    }

    public void EnemyHeal(float health)
    {
        enemyHealth += health;
        UpdateHeatlhBar();
    }

    void Die() {
        Destroy(gameObject);
    }

    void UpdateHeatlhBar()
    {
        healthSlider.value = enemyHealth;
    }


}
