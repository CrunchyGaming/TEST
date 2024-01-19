using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageArea : MonoBehaviour
{
    // Start is called before the first frame update
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
        else{
            Instantiate(damageArea, transform.position, new Quaternion(0f, 0f, 0f, 0f));
        }

        DestroyPotion();

    }

    void DestroyPotion()
    {
        Destroy(gameObject);
    }

}
