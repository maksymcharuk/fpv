using UnityEngine;

namespace FPVDrone
{
    public class KeyboardDroneInput : BaseDroneInput
    {
        #region Properties
        protected float throttleInput = 0f;
        public float RawThrottleInput
        {
            get { return throttleInput; }
        }

        protected float stickyThrottleInput = 0f;
        public float StickyThrottleInput
        {
            get { return stickyThrottleInput; }
        }

        protected Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        protected float rotationInput = 0f;
        public float RotationInput
        {
            get { return rotationInput; }
        }
        #endregion

        #region Built-in Methods
        #endregion

        #region Custom Methods
        protected override void HandleInputs()
        {
            base.HandleInputs();

            HandleThrottle();
            HandleCyclic();
            HandleRotation();

            ClampInputs();
            HandleStickyThrottle();
        }

        protected virtual void HandleThrottle()
        {
            throttleInput = Input.GetAxis("Throttle");
        }

        protected virtual void HandleCyclic()
        {
            cyclicInput.y = vertical;
            cyclicInput.x = horizontal;
        }

        protected virtual void HandleRotation()
        {
            rotationInput = Input.GetAxis("Rotation");
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            rotationInput = Mathf.Clamp(rotationInput, -1f, 1f);
        }

        // Constantly adds power to the throttle input
        protected void HandleStickyThrottle()
        {
            stickyThrottleInput += throttleInput * Time.deltaTime;
            stickyThrottleInput = Mathf.Clamp01(stickyThrottleInput);
        }
        #endregion
    }
}
