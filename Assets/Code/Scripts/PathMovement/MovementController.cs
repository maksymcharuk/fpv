using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace FPVDrone
{
    public class MovementController : MonoBehaviour
    {
        #region Variables
        public Vector3 centerOfMass;
        public Path path;
        public float maxSteer = 15.0f;
        public float maxTorque = 50f;

        public List<WheelCollider> steeringWheels;
        public List<WheelCollider> torqueWheels;

        private Rigidbody rb;
        private int currentPathObjIndex = 0;
        private float distanceFromPath = 5;
        private bool isForceStopped = false;
        #endregion


        #region Built-in Methods
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = centerOfMass;
        }

        private void Update()
        {
            if (!isForceStopped)
            {
                GetSteer();
                Move();
            }
        }
        #endregion

        #region Custom Methods
        public void ForceStop()
        {
            isForceStopped = true;
            foreach (WheelCollider wheel in torqueWheels)
            {
                wheel.motorTorque = 0;
            }
        }

        private void GetSteer()
        {
            if (path.Waypoints.Count() <= 0)
            {
                return;
            }

            Vector3 steerVector = transform.InverseTransformPoint(
                new Vector3(
                    path.Waypoints[currentPathObjIndex].position.x,
                    transform.position.y,
                    path.Waypoints[currentPathObjIndex].position.z
                )
            );
            float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);

            foreach (WheelCollider wheel in steeringWheels)
            {
                wheel.steerAngle = newSteer;
            }

            if (steerVector.magnitude <= distanceFromPath)
            {
                currentPathObjIndex++;
            }

            if (currentPathObjIndex >= path.Waypoints.Count())
            {
                currentPathObjIndex = 0;
            }
        }

        private void Move()
        {
            foreach (WheelCollider wheel in torqueWheels)
            {
                wheel.motorTorque = maxTorque;
            }
        }
        #endregion
    }
}
