using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{

    [SerializeField] KeyCode toggleKeyInventory = KeyCode.I;
    [SerializeField] KeyCode toggleKeyStats = KeyCode.Tab;
    [SerializeField] GameObject uiContainer = null;

    [Header("Tabs")]
    [SerializeField] GameObject uiContainerStats = null;
    [SerializeField] GameObject uiContainerInventory = null;
    [SerializeField] GameObject uiContainerSkills = null;



    void Start()
    {
        uiContainer.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(toggleKeyStats))
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
            uiContainerStats.SetActive(true);
            uiContainerInventory.SetActive(false);
            uiContainerSkills.SetActive(false);
        }

        if (Input.GetKeyDown(toggleKeyInventory)) {
            uiContainer.SetActive(!uiContainer.activeSelf);
            uiContainerStats.SetActive(false);
            uiContainerInventory.SetActive(true);
            uiContainerSkills.SetActive(false);
        }
    }
}
