using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public class Equipment
    {
        public event Action EquipmentUpdated;
        private readonly Dictionary<EquipLocation, EquipableItem> _equippedItems;

        protected Equipment()
        {
            _equippedItems = new Dictionary<EquipLocation, EquipableItem>();
        }
        
        
        public EquipableItem GetItemInSlot(EquipLocation equipLocation)
        {
            if (!_equippedItems.ContainsKey(equipLocation))
            {
                return null;
            }

            return _equippedItems[equipLocation];
        }

        public bool IsItemEquipped(EquipLocation location)
        {
            return _equippedItems.ContainsKey(location);
        }
        
        public void AddItem(EquipLocation slot, EquipableItem item)
        {
            Debug.Assert(item.GetAllowedEquipLocation() == slot);
            _equippedItems[slot] = item;
            EquipmentUpdated?.Invoke();
        }
        
        
        public void RemoveItem(EquipLocation slot)
        {
            if (_equippedItems.ContainsKey(slot))
            {
                _equippedItems.Remove(slot);
            }
            EquipmentUpdated?.Invoke();
        }
        
        protected IEnumerable<EquipLocation> GetAllPopulatedSlots()
        {
            return _equippedItems.Keys;
        }
    }
}