using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{

    [SerializeField] KeyCode toggleKey = KeyCode.I;
    [SerializeField] KeyCode toggleKeyAlt = KeyCode.Tab;
    [SerializeField] GameObject uiContainer = null;
  

    
    void Start()
    {
        uiContainer.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(toggleKey) || Input.GetKeyDown(toggleKeyAlt))
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
        }
    }
}
