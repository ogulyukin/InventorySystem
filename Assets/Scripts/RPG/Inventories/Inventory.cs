using System;
using UnityEngine;

namespace RPG.Inventories
{
    public class Inventory
    {
        public event Action InventoryUpdated;
        private int _inventorySize;
        protected InventorySlot[] slots;

        public Inventory(int size)
        {
            _inventorySize = size;
            slots = new InventorySlot[_inventorySize];
        }

        protected struct InventorySlot
        {
            public InventoryItem item;
            public int number;
        }

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }
        
        public int GetSize()
        {
            return _inventorySize;
        }
        
        public bool AddToFirstEmptySlot(InventoryItem item, int number)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i].item = item;
            slots[i].number += number;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
            return true;
        }
        
        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot].item;
        }
        
        public int GetNumberInSlot(int slot)
        {
            return slots[slot].number;
        }
        
        public void RemoveFromSlot(int slot, int number)
        {
            slots[slot].number -= number;
            if (slots[slot].number <= 0)
            {
                slots[slot].number = 0;
                slots[slot].item = null;
            }
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }
        }
        
        public bool AddItemToSlot(int slot, InventoryItem item, int number)
        {
            return StandardAddingToSlot(slot, item, number);
        }

        private bool StandardAddingToSlot(int slot, InventoryItem item, int number)
        {
            if (slots[slot].item != null)
            {
                return AddToFirstEmptySlot(item, number);
            }

            var i = FindStack(item);
            if (i >= 0)
            {
                slot = i;
            }

            slots[slot].item = item;
            slots[slot].number += number;
            if (InventoryUpdated != null)
            {
                InventoryUpdated();
            }

            return true;
        }
        
        public bool SetNewInventorySize(int size)
        {
            if (size < _inventorySize && _inventorySize - size > GetFreeSlotsNumber())
            {
                return false;
            }
            var newSlots = new InventorySlot[size];
            var j = 0;
            for (var i = 0; i < slots.Length; i++)
            {
                if(slots[i].item == null)
                    continue;
                newSlots[j] = slots[i];
                j++;
            }
            _inventorySize = size;
            slots = newSlots;
            return true;
        }

        private int FindSlot(InventoryItem item)
        {
            int i = FindStack(item);
            if (i < 0)
            {
                i = FindEmptySlot();
            }
            return i;
        }
        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindStack(InventoryItem item)
        {
            if (!item.IsStackable())
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
        }

        private int GetFreeSlotsNumber()
        {
            var result = 0;
            foreach (var slot in slots)
            {
                if (slot.item == null)
                    result++;
            }
            return result;
        }
    }
}