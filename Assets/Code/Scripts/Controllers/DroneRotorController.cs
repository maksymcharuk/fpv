using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPVDrone
{
    public class DroneRotorController : MonoBehaviour
    {
        #region Variables
        private List<IDroneRotor> rotors;
        #endregion

        #region Built-in Methods
        void Start()
        {
            rotors = GetComponentsInChildren<IDroneRotor>().ToList();
        }
        #endregion

        #region Custom Methods
        public void UpdateRotor(InputController input, float currentRPM)
        {
            float dps = currentRPM * 360 / 60 * Time.deltaTime;

            if (rotors.Count > 0)
            {
                foreach (IDroneRotor rotor in rotors)
                {
                    rotor.UpdateRotor(dps);
                }
            }
        }
        #endregion
    }
}
