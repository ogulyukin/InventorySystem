using RPG.Character;
using RPG.Inventories;
using RPG.Utils.UI.Dragging;
using UnityEngine;

namespace RPG.UI.Inventories
{
    public class InventoryDropTarget : MonoBehaviour, IDragDestination<InventoryItem>
    {
        [SerializeField] private PlayerEntity playerEntity;
        public void AddItems(InventoryItem item, int number)
        {
            playerEntity.ItemDropper.DropItem(item, number);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }
    }
}