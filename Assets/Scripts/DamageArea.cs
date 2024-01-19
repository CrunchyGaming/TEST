using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    List<EnemyHealth> enemiesInRange = new List<EnemyHealth>();
    public float damageAmount = 10f;
    public float durationOfArea = 8f;

    void Start()
    {
        Invoke("DestroyArea", durationOfArea);
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (collision.gameObject.CompareTag("Enemy") && !enemiesInRange.Contains(enemyHealth))
        {
            enemiesInRange.Add(enemyHealth);
            if (enemiesInRange.Count == 1)
            {
                // If this is the first enemy, start dealing damage
                InvokeRepeating("DealDamage", 0f, 1f);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (collision.gameObject.CompareTag("Enemy") && enemiesInRange.Contains(enemyHealth))
        {
            enemiesInRange.Remove(enemyHealth);
            if (enemiesInRange.Count == 0)
            {
                // If no enemies are left, stop dealing damage
                CancelInvoke("DealDamage");
            }
        }
    }

    void DealDamage()
    {
        // Use a temporary list to avoid modifying the original list while iterating
        List<EnemyHealth> enemiesToRemove = new List<EnemyHealth>();

        foreach (EnemyHealth enemy in enemiesInRange)
        {
            if (enemy != null && enemy.IsAlive())
            {
                enemy.EnemyTakeDamage(damageAmount);
            }
            else
            {
                // Mark dead enemies for removal
                enemiesToRemove.Add(enemy);
            }
        }

        // Remove dead enemies from the list
        foreach (EnemyHealth enemyToRemove in enemiesToRemove)
        {
            enemiesInRange.Remove(enemyToRemove);
        }

        if (enemiesInRange.Count == 0)
        {
            // If no enemies are left, stop dealing damage
            CancelInvoke("DealDamage");
        }
    }

    void DestroyArea()
    {
        Destroy(gameObject);
    }
}

