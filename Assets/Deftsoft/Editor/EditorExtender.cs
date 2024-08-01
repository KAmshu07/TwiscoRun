using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorExtender : MonoBehaviour
{
    [CustomEditor(typeof(Red_Text))]
    public class VNLTextEditor : UnityEditor.UI.TextEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Red_Text vnlText = (Red_Text)target;
            vnlText.locationzationID = EditorGUILayout.TextField("Locationzation ID", vnlText.locationzationID);
        }
    }
}
