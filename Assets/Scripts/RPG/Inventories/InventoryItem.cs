using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] private string itemID;
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] private string displayName;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField][TextArea] private string description;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] private Sprite icon;
        [Tooltip("The prefab that should be spawned when this item is dropped.")]
        [SerializeField] private Pickup pickup;
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] private bool stackable;
        [SerializeField] protected float itemWeight;


        static Dictionary<string, InventoryItem> _itemLookupCache;
        
        public static InventoryItem GetFromID(string itemID)
        {
            if (_itemLookupCache == null)
            {
                _itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (_itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(
                            $"Looks like there's a duplicate ID for objects: {_itemLookupCache[item.itemID]} and {item}");
                        continue;
                    }

                    _itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !_itemLookupCache.ContainsKey(itemID)) return null;
            return _itemLookupCache[itemID];
        }
        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickupInstance = Instantiate(pickup);
            pickupInstance.transform.position = position;
            pickupInstance.AddItemToPickUp(this, number);
            return pickupInstance;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public bool IsStackable()
        {
            return stackable;
        }
        
        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
    }
}
