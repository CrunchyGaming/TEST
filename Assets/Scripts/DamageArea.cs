using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    EnemyHealth enemyHealth;
    public float damageAmount = 10f;
    public float durationOfArea = 8f;
    // Start is called before the first frame update

    void Start()
    {
        Invoke("DestroyArea", durationOfArea);
    }

    void OnCollisionEnter(Collision collision) 
    {
        enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (collision.gameObject.CompareTag("Enemy"))
        {
            InvokeRepeating("DealDamage", 0f, 1f);
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            CancelInvoke("DealDamage");
        }
    }

    void DealDamage()
    {
        enemyHealth.EnemyTakeDamage(damageAmount);
    }


    void DestroyArea()
    {
        Destroy(gameObject);
    }

}
