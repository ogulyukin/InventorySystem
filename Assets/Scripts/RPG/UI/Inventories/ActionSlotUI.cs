using RPG.Character;
using RPG.Inventories;
using RPG.Utils.UI.Dragging;
using UnityEngine;
using Zenject;

namespace RPG.UI.Inventories
{
    public sealed class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] private InventoryItemIcon icon;
        [SerializeField] private int index;
        
        private ActionStore _store;
        private PlayerEntity _playerEntity;

        [Inject]
        private void Construct(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }
        
        private void Start()
        {
            _store = _playerEntity.ActionStore;
            _store.StoreUpdated += UpdateIcon;
        }

        ~ActionSlotUI()
        {
            _store.StoreUpdated -= UpdateIcon;
        }
        

        public void AddItems(InventoryItem item, int number)
        {
            _store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return _store.GetAction(index);
        }

        public int GetNumber()
        {
            return _store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return _store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            _store.RemoveItems(index, number);
        }
        

        private void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}
