using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] private EquipLocation allowedEquipLocation = EquipLocation.Weapon;
        [SerializeField] private int[] itemPrefabIndexes;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private bool externalItem;
        [SerializeField] private float strengthRequired;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }

        public int[] GetItemPrefabIndexes()
        {
            return itemPrefabIndexes;
        }

        public GameObject GetItemPrefab()
        {
            return itemPrefab;
        }

        public bool IsExternal()
        {
            return externalItem;
        }

        public float GetStrengthRequired()
        {
            return strengthRequired;
        }
    }
}