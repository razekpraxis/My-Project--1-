using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts

{
    [Serializable]
    public class StoredItem
    {
        public InventoryDefinition Details;
        public ItemVisual RootVisual;
    }



    public sealed class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance;

        private bool m_IsInventoryReady;
        public List<StoredItem> StoredItems = new List<StoredItem>();

        

        private VisualElement m_Root;
        private VisualElement m_InventoryGrid;
        public Dimensions InventoryDimensions;
        public static Dimensions SlotDimension { get; private set; }
        private static Label m_ItemDetailHeader;
        private static Label m_ItemDetailBody;
        private static Label m_ItemDetailPrice;
        private VisualElement m_Telegraph;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Configure();
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        private void Start() => LoadInventory();

        private async void LoadInventory()
        {
            await UniTask.WaitUntil(() => m_IsInventoryReady);

            foreach (StoredItem loadedItem in StoredItems)
            {
                ItemVisual inventoryItemVisual = new ItemVisual(loadedItem.Details);

                AddItemToInventoryGrid(inventoryItemVisual);

                bool inventoryHasSpace = await GetPositionForItem(inventoryItemVisual);

                if(!inventoryHasSpace)
                {
                    Debug.Log($"No space in inventory for item {loadedItem.Details.FriendlyName}");
                    RemoveItemFromInventoryGrid(inventoryItemVisual);
                    continue;
                }

                ConfigureInventoryItem(loadedItem, inventoryItemVisual);
            }
        }
        
        private void AddItemToInventoryGrid(VisualElement item) => m_InventoryGrid.Add(item);
        private void RemoveItemFromInventoryGrid(VisualElement item) => m_InventoryGrid.Remove(item);

        private static void ConfigureInventoryItem(StoredItem item, ItemVisual visual)
        {
            item.RootVisual = visual;
            visual.style.visibility = Visibility.Visible;
        }
        

        private async void Configure()
        {
            m_Root = GetComponentInChildren<UIDocument>().rootVisualElement;
            m_InventoryGrid = m_Root.Q<VisualElement>("Grid");

            VisualElement itemDetails = m_Root.Q<VisualElement>("ItemDetails");

            m_ItemDetailHeader = itemDetails.Q<Label>("Header");
            m_ItemDetailBody = itemDetails.Q<Label>("Body");
            m_ItemDetailPrice = itemDetails.Q<Label>("SellPrice");

            await UniTask.WaitForEndOfFrame();

            ConfigureSlotDimensions();
            m_IsInventoryReady = true;
        }

        private void ConfigureSlotDimensions()
        {
            VisualElement FirstSlot = m_InventoryGrid.Children().First();

            SlotDimension = new Dimensions
            {
                Width = Mathf.RoundToInt(FirstSlot.worldBound.width),
                Height = Mathf.RoundToInt(FirstSlot.worldBound.height)
            };

        }

        



    // This method checks for an available slot in the inventory grid and places the new item there if found. 
        private async Task<bool> GetPositionForItem(VisualElement newItem)
        {
            for (int y = 0; y < InventoryDimensions.Height; y++)
            {
                for (int x = 0; x < InventoryDimensions.Width; x++)
                {
                    SetItemPosition(newItem, new Vector2(SlotDimension.Width * x, SlotDimension.Height * y)); // Set the position of the new item to the current slot being checked.
                    await UniTask.WaitForEndOfFrame();
                    
                    StoredItem overlappingItem = StoredItems.FirstOrDefault(s => s.RootVisual != null && s.RootVisual.layout.Overlaps(newItem.layout)); // Check if there is an overlapping item in the current slot.
                    // If there is no overlapping item, we can place the new item here.
                    if (overlappingItem == null)
                    {
                        return true;

                    }
                }
            }
            return false;
        }

        private static void SetItemPosition(VisualElement element, Vector2 vector)
        {
            element.style.left = vector.x;
            element.style.top = vector.y;
        }
    }
}
