using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] float minPlayerDistance = 1.5f;
    [SerializeField] GameObject player;
    Animator animator;
    Vector3 targetPos;

    EnemyHealth enemyHealth;
    public bool canTrackPlayer = true;

    NavMeshAgent agent;



    void Start() {
        animator = GetComponent<Animator>();    
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update() {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        //
        Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPos);
        //

        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(canTrackPlayer)
        {
            agent.destination = player.transform.position;
        }

        if(playerDistance <= minPlayerDistance) 
        {
            animator.SetBool("attack", true);
        } else {
            animator.SetBool("attack", false);
        }
    }

    public void Flea()
    {
        canTrackPlayer = false;
        agent.destination = player.transform.position + new Vector3(Random.Range(10.0f, 20.0f) * Random.Range(-1f, 1f), 0, Random.Range(10.0f, 20.0f) * Random.Range(-1f, 1f));
    }

}
