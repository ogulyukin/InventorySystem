using UnityEngine;

namespace RPG.Inventories
{
    public abstract class ActionItem : InventoryItem
    {
        [Tooltip("Does an instance of this item get consumed every time it's used.")]
        [SerializeField] bool consumable;
        
        public abstract void Use(GameObject user);

        public bool IsConsumable()
        {
            return consumable;
        }
    }
}