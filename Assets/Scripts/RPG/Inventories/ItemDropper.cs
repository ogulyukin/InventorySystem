using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    public class ItemDropper
    {
        private Pickup _droppedItems;
        private List<Pickup> _droppedPickups = new List<Pickup>();
        private List<DropRecord> _otherSceneDroppedItems = new List<DropRecord>();
        protected readonly Transform transform;
        
        public ItemDropper(Transform transform)
        {
            this.transform = transform;
        }
        
        public void DropItem(InventoryItem item, int number)
        {
            SpawnPickup(item, GetDropLocation(), number);
        }

        protected virtual Vector3 GetDropLocation()
        {
            return transform.position;
        }

        private void FillPickup(InventoryItem item, Vector3 spawnLocation, int number)
        {
            if (_droppedItems == null)
            {
                _droppedItems = item.SpawnPickup(spawnLocation, number);
                return;
            }
            
            _droppedItems.AddItemToPickUp(item, number);
        }

        private void SpawnPickup(InventoryItem item, Vector3 spawnLocation, int number)
        {
            var pickup = item.SpawnPickup(spawnLocation, number);
            _droppedPickups.Add(pickup);
        }


        private void RemoveDestroyedDrops()
        {
            var newList = new List<Pickup>();
            foreach (var item in _droppedPickups)
            {
                if (!item.IsPickupEmpty())
                {
                    newList.Add(item);
                }
            }
            _droppedPickups = newList;
        }
    }
}