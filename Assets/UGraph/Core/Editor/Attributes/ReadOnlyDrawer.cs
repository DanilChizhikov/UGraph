using UnityEditor;
using UnityEngine;

namespace DTech.UGraph.Core.Editor
{
    [CustomPropertyDrawer(typeof(Unity.Collections.ReadOnlyAttribute))]
    internal sealed class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndDisabledGroup();
        }
    }
}