using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageArea : MonoBehaviour
{

public GameObject damageArea;

    void Start()
    {
        Invoke("DestroyPotion", 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            return;
        }
        else
        {
            // Check if the collided object is on the "DamageArea" layer
            if (collision.gameObject.layer == LayerMask.NameToLayer("DamageArea"))
            {
                return;
            }
                Vector3 spawnPosition = new Vector3(
                transform.position.x,
                collision.contacts[0].point.y, // Use the collision point's y-coordinate
                transform.position.z
            );

        Instantiate(damageArea, spawnPosition, Quaternion.identity);
        }

        DestroyPotion();
    }

    void DestroyPotion()
    {
        Destroy(gameObject);
    }

}
