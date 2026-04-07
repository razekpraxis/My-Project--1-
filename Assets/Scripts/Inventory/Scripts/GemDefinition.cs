using UnityEngine;

[CreateAssetMenu(fileName = "NewGemItem", menuName = "Inventory/Item/Gem")]
public class GemItem : InventoryDefinition
{
    public GemType Type;

    public enum GemType
    {
        Ruby,
        Sapphire,
        Emerald,
        Diamond,
        Amethyst
    }
    public enum GemElement
    {
        Fire,
        Water,
        Earth,
        Air,
        Lightning
    }
    public int PowerLevel;
}
