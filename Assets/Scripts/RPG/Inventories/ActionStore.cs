using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public sealed class ActionStore
    {
        private readonly Dictionary<int, DockedItemSlot> _dockedItems;

        public ActionStore()
        {
            _dockedItems = new Dictionary<int, DockedItemSlot>();
        }
        private class DockedItemSlot 
        {
            public ActionItem item;
            public int number;
        }
        
        public event Action StoreUpdated;
        
        public ActionItem GetAction(int index)
        {
            if (_dockedItems.ContainsKey(index))
            {
                return _dockedItems[index].item;
            }
            return null;
        }
        
        public int GetNumber(int index)
        {
            if (_dockedItems.ContainsKey(index))
            {
                return _dockedItems[index].number;
            }
            return 0;
        }
        public void AddAction(InventoryItem item, int index, int number)
        {
            if (_dockedItems.ContainsKey(index))
            {  
                if (ReferenceEquals(item, _dockedItems[index].item))
                {
                    _dockedItems[index].number += number;
                }
            }
            else
            {
                var slot = new DockedItemSlot
                {
                    item = item as ActionItem,
                    number = number
                };
                _dockedItems[index] = slot;
            }
            if (StoreUpdated != null)
            {
                StoreUpdated();
            }
        }

        public bool Use(int index, GameObject user)
        {
            if (_dockedItems.ContainsKey(index))
            {
                _dockedItems[index].item.Use(user);
                if (_dockedItems[index].item.IsConsumable())
                {
                    RemoveItems(index, 1);
                }
                return true;
            }
            return false;
        }
        
        public void RemoveItems(int index, int number)
        {
            if (_dockedItems.ContainsKey(index))
            {
                _dockedItems[index].number -= number;
                if (_dockedItems[index].number <= 0)
                {
                    _dockedItems.Remove(index);
                }
                if (StoreUpdated != null)
                {
                    StoreUpdated();
                }
            }
        }
        
        public int MaxAcceptable(InventoryItem item, int index)
        {
            var actionItem = item as ActionItem;
            if (!actionItem) return 0;

            if (_dockedItems.ContainsKey(index) && !ReferenceEquals(item, _dockedItems[index].item))
            {
                return 0;
            }
            if (actionItem.IsConsumable())
            {
                return int.MaxValue;
            }
            if (_dockedItems.ContainsKey(index))
            {
                return 0;
            }

            return 1;
        }
    }
}