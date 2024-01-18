using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 50f;
    EnemyHealth enemyHealth;
    PlayerControls playerControls;
    bool hasDealtDamage = false;
    bool isAttacking;
    [SerializeField] GameObject player;



    void Awake() {
        playerControls = player.GetComponent<PlayerControls>();
    }

    void Update() {
        isAttacking = playerControls.isAttacking;
    }

    void OnCollisionEnter(Collision collision) {
        if (!hasDealtDamage) {
            // Check if the collided object has an EnemyHealth component
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null && isAttacking) {
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
