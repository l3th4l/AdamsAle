////namespace Dialogue.Editor
////{
////    using System;
////    using UnityEditor;
////    using UnityEngine;

////    [CustomPropertyDrawer(typeof(DialogueItem))]
////    internal sealed class DialogueItemDrawer : PropertyDrawer
////    {
////        private readonly string[] popupOptions =
////        {
////            "Use Text",
////            "Use Event"
////        };

////        private GUIStyle popupStyle;

////        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
////        {
////            if (this.popupStyle == null)
////            {
////                this.popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
////                {
////                    imagePosition = ImagePosition.ImageOnly
////                };
////            }

////            label = EditorGUI.BeginProperty(position, label, property);
////            position = EditorGUI.PrefixLabel(position, label);

////            EditorGUI.BeginChangeCheck();

////            // Get properties
////            SerializedProperty usage = property.FindPropertyRelative("Use");
////            SerializedProperty text = property.FindPropertyRelative("Text");
////            SerializedProperty @event = property.FindPropertyRelative("Event");

////            // Calculate rect for configuration button
////            Rect buttonRect = new Rect(position);
////            buttonRect.yMin += this.popupStyle.margin.top;
////            buttonRect.width = this.popupStyle.fixedWidth + this.popupStyle.margin.right;
////            position.xMin = buttonRect.xMax;

////            // Store old indent level and set it to 0, the PrefixLabel takes care of it
////            int indent = EditorGUI.indentLevel;
////            EditorGUI.indentLevel = 0;

////            EditorGUI.PropertyField(buttonRect, usage);
            
////            if (usage.enumValueIndex == 0)
////            {
////                EditorGUI.PropertyField(position, text);
////            }

////            if (usage.enumValueIndex == 1)
////            {
////                EditorGUI.PropertyField(position, @event);
////            }

////            if (EditorGUI.EndChangeCheck())
////            {
////                property.serializedObject.ApplyModifiedProperties();
////            }

////            EditorGUI.indentLevel = indent;
////            EditorGUI.EndProperty();
////        }
////    }
////}