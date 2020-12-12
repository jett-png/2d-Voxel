using System.Collections;
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    [CustomEditor(typeof(CameraController))]
    public class Camera : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CameraController cam = (CameraController)target;

            if (GUILayout.Button("Locate Player"))
                cam.SnapToTarget();
        }
    }
}