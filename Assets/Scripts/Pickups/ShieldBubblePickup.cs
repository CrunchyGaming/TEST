using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubblePickup : MonoBehaviour
{

    GameObject player;
    GameObject shieldBubble;



    //change the number to be whatever index the shieldBubble is
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.gameObject;

            player.GetComponent<PlayerHealth>().enabled = false;
            shieldBubble = player.transform.Find("ShieldBubble").gameObject;
            shieldBubble.SetActive(true);

            StartCoroutine(ShieldTimer());
            Destroy(this.gameObject);

        }
    }

    IEnumerator ShieldTimer() {

        yield return new WaitForSeconds(10f);

        DisableShield();

    }

    void DisableShield() {
        player.GetComponent<PlayerHealth>().enabled = true;
        shieldBubble?.SetActive(false);
    }

}
