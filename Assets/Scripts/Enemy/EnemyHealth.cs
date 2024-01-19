using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float enemyHealth = 100f;
    [SerializeField] float timeToHeal = 8f;

    public Slider healthSlider;

    EnemyMovement enemyMovement;
  

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void EnemyTakeDamage(float damage)
    {
        enemyHealth -= damage;
        UpdateHeatlhBar();

        if (enemyHealth <= 20)
        {
            enemyMovement.Flea();
            Invoke("HealFleadEnemy", timeToHeal);
        }

        if (enemyHealth <= 0) {
            Die();
        }

    }

    public bool IsAlive()
    {
        return enemyHealth > 0;
    }

    void Die() {
        Destroy(gameObject);
    }

    void UpdateHeatlhBar()
    {
        healthSlider.value = enemyHealth;
    }

    void HealFleadEnemy()
    {
        enemyHealth = 100f;
        UpdateHeatlhBar();
        enemyMovement.canTrackPlayer = true;
    }

}
