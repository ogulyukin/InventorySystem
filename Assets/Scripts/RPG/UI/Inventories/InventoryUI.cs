using RPG.Character;
using RPG.Inventories;
using UnityEngine;

namespace RPG.UI.Inventories
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private PlayerEntity player;
        [SerializeField] private InventorySlotUI inventoryItemPrefab;
        
        private Inventory _targetInventory;
        

        private void Start()
        {
            _targetInventory = player.Inventory;
            _targetInventory.InventoryUpdated += Redraw;
            Redraw();
        }

        ~InventoryUI()
        {
            _targetInventory.InventoryUpdated -= Redraw;
        }


        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _targetInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(inventoryItemPrefab, transform);
                itemUI.Setup(_targetInventory, i);
            }
        }
    }
}