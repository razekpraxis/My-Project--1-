using UnityEngine;
using System;


// create an item definition that can be used to create inventory items. 
[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item/Test")]
public class ItemDefinition : ScriptableObject
{
    public string _itemID = Guid.NewGuid().ToString();
    public string _itemName;
    public string _itemDescription;
    public Sprite _icon;
    public Dimensions _slotDimension;
}


// create a new item called Gem that inherits from the item definition and adds new properties specific to the gem item




// creates a new struct called dimensions that can be used to define the width and height of an inventory slot. 
[Serializable]
public struct Dimensions
{
    public int Width;
    public int Height;

    
}

