using RPG.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace System
{
    public sealed class MouseInputController: ITickable
    {
        private bool _isDragging;
        private readonly CursorMapping[] _cursorMappings;

        public MouseInputController(SceneViewConfig config)
        {
            _cursorMappings = config.cursorMappings;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonUp(0)) _isDragging = false;
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0)) _isDragging = true;
                SetCursor(CursorType.UI);
            }
            if (_isDragging) return;
            SetCursor(CursorType.None);
        }
        
        private void SetCursor(CursorType type)
        {
            var mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            for (var i = 0; i < _cursorMappings.Length; i++)
            {
                if (_cursorMappings[i].type == type)
                {
                    return _cursorMappings[i];
                }
            }
            return _cursorMappings[0];
        }
    }
}
