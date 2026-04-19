using System;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemDefinition item;
    private void Awake()
    {
        Debug.Log($"Item Container Awake: {item._itemName}");
        Debug.Log("Item Container Awake: " + item._itemName);
    }
}
