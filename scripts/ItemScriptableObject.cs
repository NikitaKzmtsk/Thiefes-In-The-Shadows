using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Default, Flashlight }
public class ItemScriptableObject : ScriptableObject
{
    
    public string itemName;
    public int maxAmount;
    public GameObject itemPrefab;
    public Sprite icon;
    public ItemType itemType;
    public int price;
}
