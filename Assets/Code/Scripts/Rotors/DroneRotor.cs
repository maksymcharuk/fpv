using UnityEngine;

namespace FPVDrone
{
    public class DroneRotor : MonoBehaviour, IDroneRotor
    {
        #region Variables
        #endregion

        #region Properties
        private float currentRPM;
        public float CurrentRPM
        {
            get { return currentRPM; }
        }
        #endregion

        #region Interface Methods
        public void UpdateRotor(float dps)
        {
            currentRPM = dps / 360 * 60;
            transform.Rotate(Vector3.up, dps);
        }
        #endregion
    }
}
