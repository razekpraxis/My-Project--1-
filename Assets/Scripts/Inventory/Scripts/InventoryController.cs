using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEngine;


namespace Assets.Scripts
{
    [Serializable]
    public class StoredItem
    {
        public ItemDefinition Details;
        public ItemVisual RootVisual;
    }
}

public sealed class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    
    public List<StoredItem> StoredItems = new List<StoredItem>();

    private VisualElement m_Root;
    private VisualElement m_InventoryGrid;
    public Dimensions InventoryDimensions;
    public static Dimensions SlotDimension {get; private set;}
    private VisualElement m_Telegraph;
    private const int tileSizeWidth = 1;
    private const int tileSizeHeight = 1;
    public const int gridSizeWidth = 9;
    private const int gridSizeMaxHeight = 6; 
    public int gridSizeHeight = 3;
    private bool m_IsInventoryReady = false;

    // Singleton pattern implementation for InventoryController
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
    
    private async Task Configure()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_InventoryGrid = m_Root.Q<VisualElement>("Grid");
        
        await UniTask.WaitForEndOfFrame();

        ConfigureSlotDimensions();

        m_IsInventoryReady = true;
    }

    private void ConfigureSlotDimensions()
    {

    VisualElement firstSlot = m_InventoryGrid.Children.First();

    SlotDimension = new Dimensions
        {
            Width = Mathf.RoundToInt(firstSlot.worldBound.width),
            Height = Mathf.RoundToInt(firstSlot.worldBound.height)
        };
     
    }
    
    private void AddItemToInventoryGrid(VisualElement item) => m_InventoryGrid.Add(item);
    private void RemoveItemFromInventoryGrid(VisualElement item) => m_InventoryGrid.Remove(item);

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
                Debug.LogError($"No space for item: {loadedItem.Details._itemName}");
                RemoveItemFromInventoryGrid(inventoryItemVisual);
                continue;
            }

            ConfigureInventoryItem(loadedItem, inventoryItemVisual);
        }
    }

    private static void ConfigureInventoryItem(StoredItem item, ItemVisual visual)
    {
        item.RootVisual = visual;
        visual.style.visibility = Visibility.Visible;
    }






}

