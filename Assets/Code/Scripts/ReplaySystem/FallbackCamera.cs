using UnityEngine;

namespace FPVDrone
{
    public class FallbackCamera : MonoBehaviour
    {
        public Transform droneTransform; // The transform of the drone
        public Vector3 offset; // The offset from the drone to the camera

        void LateUpdate()
        {
            if (droneTransform != null)
            {
                transform.position = droneTransform.position + offset;
                transform.LookAt(droneTransform); // Optional: make the camera always face the drone
            }
        }
    }
}
