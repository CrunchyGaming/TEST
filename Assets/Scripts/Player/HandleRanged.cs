using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class HandleRanged : MonoBehaviour
{

    [SerializeField] float attackRange = 5f;
    public Renderer rangeRend;
    public GameObject rangeInd;
    [SerializeField] LayerMask targetLayer;

    bool canShoot = false;

    void Update()
    {
        ProcessRange();
    }

    void ProcessRange() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Perform the raycast with the specified layerMask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer)) {
            Vector3 mousePosition = hit.point;
            mousePosition.y = transform.position.y;

            rangeInd.transform.position = mousePosition;
        }

        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, rangeInd.transform.position);
        if (distanceFromPlayer >= attackRange) {
            rangeRend.material.color = new Color(255f, 0f, 0f);
        } else {
            rangeRend.material.color = new Color(0f, 255f, 0f);
        }
    }

    public void EnableInd()
    {
        rangeInd.SetActive(true);
    }

    public void DisableInd()
    {
        rangeInd.SetActive(false);
    }

}
