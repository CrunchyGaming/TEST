using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubblePickup : MonoBehaviour
{

    [SerializeField] float healthAmount = 20f;
    GameObject player;
    GameObject shieldBubble;



    //change the number to be whatever index the shieldBubble is
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.gameObject;

            player.GetComponent<PlayerHealth>().enabled = false;
            shieldBubble = player.transform.Find("ShieldBubble").gameObject;
            shieldBubble.SetActive(true);

            Destroy(this.gameObject);

        }
    }


}
