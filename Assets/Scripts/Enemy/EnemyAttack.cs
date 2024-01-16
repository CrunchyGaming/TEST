using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 40f;
    PlayerHealth playerHealth;



    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    //in attack animation (activates this)
    public void AttackHitEvent() {
        if (playerHealth == null) return;
        playerHealth.TakeDamage(damageAmount);
    }

}
