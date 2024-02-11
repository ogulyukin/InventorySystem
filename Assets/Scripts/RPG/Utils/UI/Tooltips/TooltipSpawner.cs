using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Utils.UI.Tooltips
{
    public abstract class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("The prefab of the tooltip to spawn.")]
        [SerializeField] GameObject tooltipPrefab = null;
        GameObject _tooltip = null;
        public abstract void UpdateTooltip(GameObject tooltip);
        
        public abstract bool CanCreateTooltip();
        

        private void OnDestroy()
        {
            ClearTooltip();
        }

        private void OnDisable()
        {
            ClearTooltip();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            var parentCanvas = GetComponentInParent<Canvas>();

            if (_tooltip && !CanCreateTooltip())
            {
                ClearTooltip();
            }

            if (!_tooltip && CanCreateTooltip())
            {
                _tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);
            }

            if (_tooltip)
            {
                UpdateTooltip(_tooltip);
                PositionTooltip();
            }
        }

        private void PositionTooltip()
        {
            Canvas.ForceUpdateCanvases();

            var tooltipCorners = new Vector3[4];
            _tooltip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
            var slotCorners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(slotCorners);

            var transform1 = transform;
            bool below = transform1.position.y > Screen.height / 2;
            bool right = transform1.position.x < Screen.width / 2;

            int slotCorner = GetCornerIndex(below, right);
            int tooltipCorner = GetCornerIndex(!below, !right);

            _tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + _tooltip.transform.position;
        }

        private int GetCornerIndex(bool below, bool right)
        {
            if (below && !right) return 0;
            else if (!below && !right) return 1;
            else if (!below && right) return 2;
            else return 3;

        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ClearTooltip();
        }

        private void ClearTooltip()
        {
            if (_tooltip)
            {
                Destroy(_tooltip.gameObject);
            }
        }
    }
}