using UnityEditor;

namespace FPVDrone
{
    [CustomEditor(typeof(KeyboardDroneInput))]
    public class KeyboardDroneInputEditor : Editor
    {
        #region Variables
        KeyboardDroneInput targetInput;
        #endregion

        #region Built-in Methods
        void OnEnable()
        {
            targetInput = (KeyboardDroneInput)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawDebugLayout();

            Repaint();
        }

        void DrawDebugLayout()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(10);

            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Throttle Input: " + targetInput.RawThrottleInput);
            EditorGUILayout.LabelField("Cyclic Input: " + targetInput.CyclicInput);
            EditorGUILayout.LabelField("Pedal Input: " + targetInput.PedalInput);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();
        }
        #endregion
    }
}
