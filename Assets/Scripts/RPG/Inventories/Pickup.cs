using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Inventories
{
    public class Pickup : MonoBehaviour
    {
        private readonly Dictionary<InventoryItem, int> _items = new();
        
        private Inventory _inventory;
        
        protected void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _inventory = player.GetComponent<Inventory>();
        }

        public bool IsPickupEmpty()
        {
            return _items.Count == 0;
        }
        
        public void AddItemToPickUp(InventoryItem item, int number)
        {
            if (_items.ContainsKey(item))
            {
                if (item.IsStackable()) _items[item] += number;
            }
            else
            {
                _items.Add(item, number);
            }
        }
        
        public void Setup(Dictionary<InventoryItem, int> items)
        {
            foreach (var item in items)
            {
                items.Add(item.Key, !item.Key.IsStackable() ? 1 : item.Value);    
            }
        }

        public IEnumerable<InventoryItem> GetItems()
        {
            foreach (var item in _items)
            {
                yield return item.Key;
            }
        }

        public int GetNumber(InventoryItem item)
        {
            return _items.ContainsKey(item) ? _items[item] : 0;
        }

        public void PickupItem()
        {

            var keys = _items.Keys;
            foreach (var key in keys.ToList())
            {
                bool foundSlot = _inventory.AddToFirstEmptySlot(key, _items[key]);
                if(!foundSlot) return;
                _items.Remove(key);
            }
            
            foreach (var item in _items)
            {
                bool foundSlot = _inventory.AddToFirstEmptySlot(item.Key, item.Value);    
                if(!foundSlot) return;
                _items.Remove(item.Key);
            }
            Destroy(gameObject);
        }

        public bool CanBePickedUp()
        {
            foreach (var item in _items)
            {
                if (_inventory.HasSpaceFor(item.Key))
                {
                    return true;
                }    
            }

            return false;
        }
    }
}