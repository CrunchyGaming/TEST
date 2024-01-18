using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBubblePickup : MonoBehaviour
{
    //PLEASE DONT TOUCH THIS SHIT EVER AGAIN
    [SerializeField] float originalShieldTimer = 10f;
    float shieldTimer;
    bool isShieldActive = false;
    GameObject player;
    GameObject shieldBubble;
    GameObject canvas;
    Slider shieldSlider;
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
            shieldSlider = shieldBubbleUI.GetComponent<Slider>();
            shieldBubbleUI.SetActive(true);
            shieldTimer = originalShieldTimer;
            shieldSlider.value = originalShieldTimer;

            meshRenderer.enabled = false;
            boxCollider.enabled = false;

            isShieldActive = true;
            StartCoroutine(ShieldProcess());

        }
    }

    IEnumerator ShieldProcess() {
        yield return new WaitForSeconds(0.1f);

        while (shieldTimer > 0 && isShieldActive) {
            shieldTimer -= Time.deltaTime;
            shieldSlider.value = shieldTimer;

            yield return null;
        }

        if (shieldSlider.value <= 0) {
            DisableShield();
        }
    }

    void DisableShield() {
        isShieldActive = false;
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
