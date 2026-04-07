using UnityEngine;
using System;


// create an item definition that can be used to create inventory items. 
[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item/Test")]
public class InventoryDefinition : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string FriendlyName;
    public string Description;
    public int SellPrice;
    public Sprite Icon;
    public Dimensions SlotDimension;
}


// create a new item called Gem that inherits from the item definition and adds new properties specific to the gem item








// creates a new struct called dimensions that can be used to define the width and height of an inventory slot. 
[Serializable]
public struct Dimensions
{
    public int Width;
    public int Height;

    
}

