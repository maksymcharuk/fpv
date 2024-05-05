using UnityEngine;

namespace FPVDrone
{
    public class DroneCharacteristics : MonoBehaviour
    {
        #region Variables
        [Header("Lift Properties")]
        public float maxLiftForce = 50f;
        public DroneRotor[] rotors;

        [Space]
        [Header("Rotation Properties")]
        public float rotationForce = 2f;

        [Space]
        [Header("Cyclic Properties")]
        public float cyclicForce = 2f;
        #endregion

        #region Custom Methods
        public virtual void UpdateCharacteristics(Rigidbody rb, InputController input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandleRotation(rb, input);
        }

        protected virtual void HandleLift(Rigidbody rb, InputController input)
        {
            Vector3 liftForce = transform.up * (Physics.gravity.magnitude + maxLiftForce) * rb.mass;
            // TODO: Handle multiple rotors
            float normalizedRPM = rotors[0].CurrentRPM / 50f;
            rb.AddForce(
                liftForce * Mathf.Pow(normalizedRPM, 2f) * Mathf.Pow(input.ThrottleInput, 2f),
                ForceMode.Force
            );
        }

        protected virtual void HandleCyclic(Rigidbody rb, InputController input)
        {
            float cyclicZForce = input.CyclicInput.x * cyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);

            float cyclicXForce = input.CyclicInput.y * cyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
        }

        protected virtual void HandleRotation(Rigidbody rb, InputController input)
        {
            rb.AddTorque(transform.up * input.RotationInput * rotationForce, ForceMode.Force);
        }
        #endregion
    }
}
