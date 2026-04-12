using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Party", menuName = "Scriptable Objects/Party")]
public class Party : ScriptableObject
{
    [Header("Party Members")]
    [Tooltip("The active party members. Three characters can be in the active party at a time.")]
    public CharacterDef[] _activePartyMembers = new CharacterDef[3];

    [Header("All Party Members")]
    [Tooltip("A list of all characters in the party.")]
    public List<CharacterDef> _allPartyMembers = new List<CharacterDef>();

    [Header("ItemInventory")]
    [Tooltip("The items in the party's inventory.")]
    public List<InventoryDefinition> _itemInventory = new List<InventoryDefinition>();
    public const int _maxItemInventorySize = 32;

    [Header("EquipmentInventory")]
    [Tooltip("The equipment in the party's inventory.")]
    public List<InventoryDefinition> _equipmentInventory = new List<InventoryDefinition>();
    public const int _maxEquipmentInventorySize = 255;

    [Header("Dragoon Spirits")]
    [Tooltip("The dragoon spirits available to the party.")]
    public List<DragoonSpirit> _dragoonSpirits = new List<DragoonSpirit>();

    public void SwapCharacters(int slotIndex, CharacterDef characterToAdd) // simple method to swap characters.
    {
        _activePartyMembers[slotIndex] = characterToAdd;        
    } 
    
}
