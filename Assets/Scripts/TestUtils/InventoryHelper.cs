using RPG.Character;
using RPG.Inventories;
using UnityEngine;

namespace TestUtils
{
    public sealed class InventoryHelper : MonoBehaviour
    {
        [Tooltip("Items added at start game")] 
        [SerializeField] private InventoryItem[] startInventoryItems;
        [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private InventoryItem inventoryItem;
        [Tooltip("Number for stackable items")]
        [SerializeField] private int number = 1;
        private Inventory _playerInventory;

        private void Start()
        {
            _playerInventory = playerEntity.Inventory;
            foreach (var item in startInventoryItems)
            {
                _playerInventory.AddToFirstEmptySlot(item, 1);
            }
        }
        
        public void AddItemToInventory()
        {
            if (number < 1)
            {
                number = 1;
            }
            _playerInventory.AddToFirstEmptySlot(inventoryItem, inventoryItem.IsStackable() ? number : 1);
        }
    }
}
