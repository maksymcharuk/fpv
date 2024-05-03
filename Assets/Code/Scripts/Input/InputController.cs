using UnityEngine;

namespace FPVDrone
{
    public enum InputType
    {
        Keyboard,
        Joystick,
        Touch
    }

    public class InputController : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;

        public KeyboardDroneInput keyboardInput;
        #endregion

        #region Properties
        private float throttleInput;
        public float ThrottleInput
        {
            get { return throttleInput; }
        }

        private float stickyThrottleInput;
        public float StickyThrottle
        {
            get { return stickyThrottleInput; }
        }

        private Vector2 cyclicInput;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        private float pedalInput;
        public float PedalInput
        {
            get { return pedalInput; }
        }
        #endregion

        #region Built-in Methods
        void Start()
        {
            keyboardInput = GetComponent<KeyboardDroneInput>();

            if (keyboardInput)
            {
                SetIputType(inputType);
            }
            else
            {
                Debug.LogWarning("No KeyboardDroneInput found on this GameObject");
            }
        }

        private void Update()
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    throttleInput = keyboardInput.RawThrottleInput;
                    stickyThrottleInput = keyboardInput.StickyThrottleInput;
                    cyclicInput = keyboardInput.CyclicInput;
                    pedalInput = keyboardInput.PedalInput;
                    break;
                default:
                    Debug.LogWarning("No Input Type selected");
                    break;
            }
        }
        #endregion

        #region Custom Methods
        void SetIputType(InputType type)
        {
            if (keyboardInput)
            {
                if (type == InputType.Keyboard)
                {
                    keyboardInput.enabled = true;
                }
            }
        }
        #endregion
    }
}
