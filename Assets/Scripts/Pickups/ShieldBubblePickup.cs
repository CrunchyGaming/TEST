using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBubblePickup : MonoBehaviour
{

    [SerializeField] float shieldTimer = 10f;
    float shieldTimerSpeed = 1f;
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
            shieldTimerSpeed = shieldTimer;
            shieldSlider.value = shieldTimerSpeed;
            
            


            StartCoroutine(ShieldTimer());
            StartCoroutine(TickDownShieldTime());
            meshRenderer.enabled = false;
            boxCollider.enabled = false;

        }
    }

    IEnumerator ShieldTimer() {
        yield return new WaitForSeconds(shieldTimer);

        DisableShield();
    }

    IEnumerator TickDownShieldTime()
    {
        shieldTimerSpeed--;
        shieldSlider.value = shieldTimerSpeed;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TickDownShieldTime());
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
