using UnityEditor;
using UnityEngine;

namespace Simulation.Editor
{
    [CustomEditor(typeof(MatrixRotator))]
    public class MatrixRotatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var rotator = (MatrixRotator)target;

            if (GUILayout.Button("Rotate"))
            {
                rotator.Rotate();
            }

            if (GUILayout.Button("Reset"))
            {
                rotator.Reset();
            }
        }
    }
}
