using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
public class ItemVisual : VisualElement
{
    private readonly InventoryDefinition m_Item;

    public ItemVisual(InventoryDefinition item)
    {
        m_Item = item;
        name = $"{item._itemName}";

        style.height = m_Item._slotDimension.Height * PlayerInventory.SlotDimension.Height;
        style.width = m_Item._slotDimension.Width * PlayerInventory.SlotDimension.Width;
        style.visibility = Visibility.Hidden;
    
        VisualElement icon = new VisualElement
        {
            style = {   backgroundImage = m_Item._icon.texture}
        };
        Add(icon);

        icon.AddToClassList("visual-icon");
        AddToClassList("visual-icon-container");
    }
   

   public void SetPosition(Vector2 pos)
    {
        style.left = pos.x;
        style.top = pos.y;
    }


    }
}