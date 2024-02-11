using UnityEngine;

namespace RPG.UI.Inventories
{
    public sealed class InventoryCanvasView : MonoBehaviour
    {
        private bool _activeness = true;
        
        public void ToggleViewActiveness()
        {
            gameObject.SetActive(!_activeness);
            _activeness = !_activeness;
        }

        private void Start()
        {
            ToggleViewActiveness();
        }
    }
}
