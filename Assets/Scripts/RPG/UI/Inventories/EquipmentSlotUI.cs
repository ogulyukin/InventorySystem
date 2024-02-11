using RPG.Character;
using RPG.Inventories;
using RPG.Utils.UI.Dragging;
using UnityEngine;
using Zenject;

namespace RPG.UI.Inventories
{
    public sealed class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] private InventoryItemIcon icon;
        [SerializeField] private EquipLocation equipLocation = EquipLocation.Weapon;

        private Equipment _playerEquipment;
        private PlayerEntity _playerEntity;

        [Inject]
        private void Construct(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }
        

        private void Start() 
        {
            _playerEquipment = _playerEntity.Equipment;
            _playerEquipment.EquipmentUpdated += RedrawUI;
            RedrawUI();
        }

        public int MaxAcceptable(InventoryItem item)
        {
            EquipableItem equipableItem = item as EquipableItem;
            if (equipableItem == null) return 0;
            if (equipableItem.GetAllowedEquipLocation() != equipLocation) return 0;
            if (GetItem() != null) return 0;

            return 1;
        }

        public void AddItems(InventoryItem item, int number)
        {
            _playerEquipment.AddItem(equipLocation, (EquipableItem) item);
        }

        public InventoryItem GetItem()
        {
            return _playerEquipment.GetItemInSlot(equipLocation);
        }

        public int GetNumber()
        {
            if (GetItem() != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void RemoveItems(int number)
        {
            _playerEquipment.RemoveItem(equipLocation);
        }
        

        void RedrawUI()
        {
            icon.SetItem(_playerEquipment.GetItemInSlot(equipLocation));
        }
    }
}