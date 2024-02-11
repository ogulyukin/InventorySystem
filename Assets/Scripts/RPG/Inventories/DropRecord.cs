using UnityEngine;

namespace RPG.Inventories
{
    [System.Serializable]
    public struct DropRecord
    {
        public string itemID;
        public Vector3 position;
        public int number;
        public int sceneBuiltIndex;
    }
}