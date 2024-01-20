using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class HandleRanged : MonoBehaviour
{

    [SerializeField] float attackRange = 10f;
    [SerializeField] float powerZ = 0.45f;
    [SerializeField] float powerY = 1f;
    public Renderer rangeRend;
    public GameObject rangeInd;
    public GameObject damagePotion;
    public GameObject potionUISelector;
    [SerializeField] LayerMask targetLayer;


    float distanceFromPlayer;
    float shotPowerZ;
    float shotPowerY;
    bool canShoot = false;
    public bool isPotionButtonOn = false;

    PlayerControls playerControls;

    void Awake()
    {
        playerControls = GetComponent<PlayerControls>();
    }

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

        distanceFromPlayer = Vector3.Distance(gameObject.transform.position, rangeInd.transform.position);
        if (distanceFromPlayer >= attackRange) {
            canShoot = false;
            rangeRend.material.color = new Color(255f, 0f, 0f);
        } else {
            canShoot = true;
            rangeRend.material.color = new Color(0f, 255f, 0f);
        }
    }

    public void EnableInd()
    {
        if(isPotionButtonOn)
        {
            rangeInd.SetActive(true);
        }
    }

    public void DisableInd()
    {
        if(rangeInd.active == true && canShoot)
        {
            isPotionButtonOn = false;
            potionUISelector.SetActive(false);
            GameObject clone;
            clone = Instantiate(damagePotion, transform.position + new Vector3(0, 2f, 0), transform.rotation * new Quaternion(0f, 90f, 90f, 0f));
            shotPowerZ = powerZ * distanceFromPlayer;
            shotPowerY = powerY * distanceFromPlayer;
            clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(0, shotPowerY, shotPowerZ);
            rangeInd.SetActive(false);
        }
    }

    

    public void DisableIndWithoutFiring()
    {
        if(rangeInd.active == true)
        {
            rangeInd.SetActive(false);
        }
    }

    public void TogglePotionButton()
    {
        if(isPotionButtonOn)
        {
            isPotionButtonOn = false;
        }
        else if(!isPotionButtonOn)
        {
            isPotionButtonOn = true;
            if(playerControls.isLooking)
            {
                EnableInd();
            }
        }
    }

    public void PotionButtonOff()
    {
        isPotionButtonOn = false;
    }
}
