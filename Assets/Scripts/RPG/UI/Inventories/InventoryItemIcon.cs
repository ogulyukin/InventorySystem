using RPG.Inventories;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Inventories
{
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviour
    {
        [SerializeField] private GameObject textContainer;
        [SerializeField] private TextMeshProUGUI itemNumber;

        public void SetItem(InventoryItem item)
        {
            SetItem(item, 0);
        }

        public void SetItem(InventoryItem item, int number)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = item.GetIcon();
            }

            if (itemNumber)
            {
                if (number <= 1)
                {
                    textContainer.SetActive(false);
                }
                else
                {
                    textContainer.SetActive(true);
                    itemNumber.text = number.ToString();
                }
            }
        }
    }
}