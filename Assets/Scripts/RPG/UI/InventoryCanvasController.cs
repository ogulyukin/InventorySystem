using RPG.UI.Inventories;
using UnityEngine;
using Zenject;

namespace RPG.UI
{
    public sealed class InventoryCanvasController : ITickable
    {
        private readonly InventoryCanvasView _canvasView;

        public InventoryCanvasController(InventoryCanvasView view)
        {
            _canvasView = view;
        }


        public void Tick()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                _canvasView.ToggleViewActiveness();
            }
        }
    }
}
