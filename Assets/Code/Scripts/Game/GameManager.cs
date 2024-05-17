using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPVDrone
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public Camera mainCamera;
        public Camera replayCamera;
        #endregion

        #region Built-in Methods
        void Awake()
        {
            if (replayCamera != null)
                replayCamera.enabled = false;
        }

        void Update()
        {
            if (mainCamera == null && replayCamera != null)
            {
                replayCamera.enabled = true;
            }
        }
        #endregion
    }
}
