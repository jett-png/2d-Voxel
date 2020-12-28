using System.Collections;
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    [CustomEditor(typeof(CameraCtrl))]
    public class Camera : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CameraCtrl cam = (CameraCtrl)target;

            if (GUILayout.Button("Locate Player"))
                cam.SnapToTarget();
        }
    }
}