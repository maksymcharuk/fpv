using UnityEngine;

namespace FPVDrone
{
    public enum InputType
    {
        Keyboard,
        Gamepad,
        Touch
    }

    public class InputController : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public InputType inputType = InputType.Keyboard;

        public KeyboardDroneInput keyboardInput;
        public GamepadDroneInput gamepadInput;
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

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }
        #endregion

        #region Built-in Methods
        void Start()
        {
            keyboardInput = GetComponent<KeyboardDroneInput>();
            gamepadInput = GetComponent<GamepadDroneInput>();

            if (keyboardInput && gamepadInput)
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
                    rotationInput = keyboardInput.RotationInput;
                    break;
                case InputType.Gamepad:
                    throttleInput = gamepadInput.RawThrottleInput;
                    // stickyThrottleInput = gamepadInput.StickyThrottleInput;
                    cyclicInput = gamepadInput.CyclicInput;
                    rotationInput = gamepadInput.RotationInput;
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
            if (keyboardInput && gamepadInput)
            {
                if (type == InputType.Keyboard)
                {
                    keyboardInput.enabled = true;
                    gamepadInput.enabled = false;
                }
                else if (type == InputType.Gamepad)
                {
                    keyboardInput.enabled = false;
                    gamepadInput.enabled = true;
                }
            }
        }
        #endregion
    }
}
