using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 50f;
    EnemyHealth enemyHealth;
    bool hasDealtDamage = false;



    void OnCollisionEnter(Collision collision) {
        if (!hasDealtDamage) {
            // Check if the collided object has an EnemyHealth component
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null) {
                enemyHealth.EnemyTakeDamage(damageAmount);
                hasDealtDamage = true;
            }
        }
    }

    void OnCollisionExit(Collision collision) {
        if (hasDealtDamage) {
            hasDealtDamage = false;
        }
    }

}
