using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    GameObject player;
    float enemySpeed;
    Vector3 targetPos;

    void Start()
    {
        enemySpeed = 1f;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player = collision.gameObject;
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPos);
            MoveEnemy();
        }
        
    }

    void MoveEnemy()
    {
        transform.position = transform.position + (transform.forward * enemySpeed) * Time.deltaTime;
    }
}
