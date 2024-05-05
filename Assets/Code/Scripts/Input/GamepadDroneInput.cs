using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPVDrone
{
    public class GamepadDroneInput : KeyboardDroneInput
    {
        #region Variables
        #endregion

        #region Custom Methods
        protected override void HandleThrottle()
        {
            throttleInput =
                Input.GetAxis("GamepadThrottleUp") + Input.GetAxis("GamepadThrottleDown");
        }

        protected override void HandleCyclic()
        {
            cyclicInput.y = Input.GetAxis("GamepadCyclicVertical");
            cyclicInput.x = Input.GetAxis("GamepadCyclicHorizontal");
        }

        protected override void HandleRotation()
        {
            rotationInput = Input.GetAxis("GamepadRotation");
        }
        #endregion
    }
}
