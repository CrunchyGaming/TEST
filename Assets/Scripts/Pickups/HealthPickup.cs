using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField] float healthAmount = 20f;
    PlayerHealth health;



    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            health = other.GetComponent<PlayerHealth>();

            health.AddHealth(healthAmount);
            Destroy(gameObject);
            
        }
    }

}
