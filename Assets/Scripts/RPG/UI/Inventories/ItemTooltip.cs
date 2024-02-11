using RPG.Inventories;
using TMPro;
using UnityEngine;

namespace RPG.UI.Inventories
{
    public class ItemTooltip : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI titleText;
        [SerializeField] public TextMeshProUGUI bodyText;

        public void Setup(InventoryItem item)
        {
            titleText.text = item.GetDisplayName();
            bodyText.text = item.GetDescription();
        }
    }
}
