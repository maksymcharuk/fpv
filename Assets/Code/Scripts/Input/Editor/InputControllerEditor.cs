using UnityEditor;

namespace FPVDrone
{
    [CustomEditor(typeof(InputController))]
    public class InputControllerEditor : Editor
    {
        #region Variables
        InputController inputCtrl;
        #endregion

        #region Built-in Methods
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            inputCtrl = (InputController)target;

            DrawDebugLayout();

            Repaint();
        }

        void DrawDebugLayout()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(10);

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Throttle Input: " + inputCtrl.ThrottleInput);
            EditorGUILayout.LabelField("Cyclic Input: " + inputCtrl.CyclicInput);
            EditorGUILayout.LabelField("Rotation Input: " + inputCtrl.RotationInput);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}
