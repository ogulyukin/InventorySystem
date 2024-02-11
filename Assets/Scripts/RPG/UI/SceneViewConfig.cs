using System;
using UnityEngine;

namespace RPG.UI
{
    [CreateAssetMenu(menuName = ("RPG/SceneViewConfig"))]
    public sealed class SceneViewConfig : ScriptableObject
    {
        public CursorMapping[] cursorMappings;
    }
    
    public enum CursorType
    {
        None, Movement, Combat, UI, PickUp, FullPickup,
        Dialogue , Lumberjack, DoorOpen, Mount, Carpentry, Mine, WaterWork, Build, CampFire, Food
    }
    
    [Serializable]
    public struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }
}
