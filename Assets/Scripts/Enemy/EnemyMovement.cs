using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    GameObject player;
    float enemySpeed = 3f;
    Vector3 targetPos;
    float minPlayerDistance = 1.5f;

    void Start()
    {
        //enemySpeed = 1f;
    }

    void OnCollisionStay(Collision collision)
    {

        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            player = collision.gameObject;
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPos);
            MoveEnemy();
        }
        
    }

    void MoveEnemy()
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(playerDistance > minPlayerDistance)
        {
            transform.position = transform.position + (transform.forward * enemySpeed) * Time.deltaTime;
        }
    }
}
