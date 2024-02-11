using UnityEngine;

namespace RPG.Inventories
{

    [System.Serializable]
    public sealed class PickUpEntry
    {
        public InventoryItem item;
        public int number;
    }
    public sealed class PickupSpawner : MonoBehaviour
    {

        [SerializeField] PickUpEntry[]  items;

        private void Awake()
        {
            SpawnPickup();
        }
        
        public Pickup GetPickup() 
        { 
            return GetComponentInChildren<Pickup>();
        }

        public bool IsCollected() 
        { 
            return GetPickup() == null;
        }
        

        private void SpawnPickup()
        {
            if(items.Length == 0) return;
            var spawnedPickup = items[0].item.SpawnPickup(transform.position, items[0].number);
            spawnedPickup.transform.SetParent(transform);
            for (int i = 1; i < items.Length; i++)
            {
                spawnedPickup.AddItemToPickUp(items[i].item, items[i].number);
            }
        }

        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }
    }
}