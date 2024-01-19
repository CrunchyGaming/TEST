using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adds asset option in unity editor to easily set object to this
[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{

    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

}
