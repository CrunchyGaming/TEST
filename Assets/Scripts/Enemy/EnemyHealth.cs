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
    }

    public void EnemyHeal(float health)
    {
        enemyHealth += health;
        UpdateHeatlhBar();
    }

    void UpdateHeatlhBar()
    {
        healthSlider.value = enemyHealth;
    }


}
