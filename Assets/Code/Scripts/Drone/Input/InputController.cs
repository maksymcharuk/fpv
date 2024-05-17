using UnityEngine;
using UnityEngine.InputSystem;

namespace FPVDrone
{
    public class InputController : MonoBehaviour
    {
        #region Variables
        private PlayerControls playerControls;
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
            playerControls = new PlayerControls();
        }

        private void OnThrottle(InputValue value)
        {
            throttleInput = value.Get<float>();
            stickyThrottleInput += throttleInput * Time.deltaTime;
            stickyThrottleInput = Mathf.Clamp01(stickyThrottleInput);
        }

        private void OnCyclic(InputValue value)
        {
            cyclicInput = value.Get<Vector2>();
        }

        private void OnRotation(InputValue value)
        {
            rotationInput = value.Get<float>();
        }
        #endregion
    }
}
