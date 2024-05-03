using UnityEngine;

namespace FPVDrone
{
    public class KeyboardDroneInput : BaseDroneInput
    {
        #region Properties
        private float throttleInput = 0f;
        public float RawThrottleInput
        {
            get { return throttleInput; }
        }

        protected float stickyThrottleInput = 0f;
        public float StickyThrottleInput
        {
            get { return stickyThrottleInput; }
        }

        private Vector2 cyclicInput = Vector2.zero;
        public Vector2 CyclicInput
        {
            get { return cyclicInput; }
        }

        private float pedalInput = 0f;
        public float PedalInput
        {
            get { return pedalInput; }
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
            HandlePedal();

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

        protected virtual void HandlePedal()
        {
            pedalInput = Input.GetAxis("Pedal");
        }

        protected void ClampInputs()
        {
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            cyclicInput = Vector2.ClampMagnitude(cyclicInput, 1f);
            pedalInput = Mathf.Clamp(pedalInput, -1f, 1f);
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
