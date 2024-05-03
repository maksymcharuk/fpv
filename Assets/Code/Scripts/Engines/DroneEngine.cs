using UnityEngine;

namespace FPVDrone
{
    public class DroneEngine : MonoBehaviour
    {
        #region Variables
        public float maxKV = 2000; // [kV]
        public float maxRPM = 22000f;
        public float powerDelay = 1f;
        public AnimationCurve powerCurve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(1f, 1f)
        );
        #endregion

        #region Properties
        private float currentKV;
        public float CurrentKV
        {
            get { return currentKV; }
        }
        private float currentRPM;
        public float CurrentRPM
        {
            get { return currentRPM; }
        }
        #endregion

        #region Built-in Methods
        void Start() { }
        #endregion

        #region Custom Methods
        public void UpdateEngine(float throttleInput)
        {
            // Calculate kV
            float wantedKV = powerCurve.Evaluate(throttleInput) * maxKV;
            currentKV = Mathf.Lerp(currentKV, wantedKV, Time.deltaTime * powerDelay);

            // Calculate RPM's
            float wantedRPM = throttleInput * maxRPM;
            currentRPM = Mathf.Lerp(currentKV, wantedRPM, Time.deltaTime * powerDelay);
        }
        #endregion
    }
}
