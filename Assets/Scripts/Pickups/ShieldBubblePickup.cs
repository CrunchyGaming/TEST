using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubblePickup : MonoBehaviour
{

    [SerializeField] float shieldTimer = 10f;
    GameObject player;
    GameObject shieldBubble;
    GameObject canvas;
    GameObject shieldBubbleUI;
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

            canvas = player.transform.Find("Canvas").gameObject;
            shieldBubbleUI = canvas.transform.Find("ShieldSlider").gameObject;
            shieldBubbleUI.SetActive(true);

            StartCoroutine(ShieldTimer());
            meshRenderer.enabled = false;
            boxCollider.enabled = false;

        }
    }

    IEnumerator ShieldTimer() {
        yield return new WaitForSeconds(shieldTimer);

        DisableShield();
    }

    void DisableShield() {
        player.GetComponent<PlayerHealth>().enabled = true;

        canvas = player.transform.Find("Canvas").gameObject;
        shieldBubbleUI = canvas.transform.Find("ShieldSlider").gameObject;
        shieldBubbleUI.SetActive(false);

        if (shieldBubble != null) {
            shieldBubble.SetActive(false);
            shieldBubbleUI.SetActive(false);
        }

        Destroy(this.gameObject);
    }

}
