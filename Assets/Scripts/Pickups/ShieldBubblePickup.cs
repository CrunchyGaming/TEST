using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubblePickup : MonoBehaviour
{

    GameObject player;
    GameObject shieldBubble;
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;



    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.gameObject;

            player.GetComponent<PlayerHealth>().enabled = false;
            shieldBubble = player.transform.Find("ShieldBubble").gameObject;
            shieldBubble.SetActive(true);

            StartCoroutine(ShieldTimer());
            meshRenderer.enabled = false;
            boxCollider.enabled = false;

        }
    }

    IEnumerator ShieldTimer() {
        yield return new WaitForSeconds(10f);

        DisableShield();
    }

    void DisableShield() {
        player.GetComponent<PlayerHealth>().enabled = true;

        shieldBubble = player.transform.Find("ShieldBubble").gameObject;

        if (shieldBubble != null) {
            shieldBubble.SetActive(false);
        }

        Destroy(this.gameObject);
    }

}
