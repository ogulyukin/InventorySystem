using UnityEditor;
using UnityEngine;

namespace TestUtils.Editor
{
    [CustomEditor(typeof(InventoryHelper))]
    public sealed class InventoryHelperEditor : UnityEditor.Editor
    {
        private SerializedProperty _inventoryItem;
        private SerializedProperty _number;
        private SerializedProperty _playerEntity;
        private SerializedProperty _startInventoryItems;

        private void OnEnable()
        {
            _startInventoryItems = serializedObject.FindProperty("startInventoryItems");
            _inventoryItem = serializedObject.FindProperty("inventoryItem");
            _number = serializedObject.FindProperty("number");
            _playerEntity = serializedObject.FindProperty("playerEntity");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var helper = (InventoryHelper) target;
            EditorGUILayout.PropertyField(_startInventoryItems, new GUIContent("Start Inventory Items"));
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(_inventoryItem, new GUIContent("Inventory Item"));
            EditorGUILayout.PropertyField(_number, new GUIContent("Amount for Stuckable items"));
            EditorGUILayout.PropertyField(_playerEntity, new GUIContent("Player Entity"));
            
            if (GUILayout.Button("Add selected item"))
            {
                helper.AddItemToInventory();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
