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
            {
                replayCamera.enabled = false;
                replayCamera.GetComponent<AudioListener>().enabled = false;
            }
        }

        void Update()
        {
            if (mainCamera == null && replayCamera != null)
            {
                replayCamera.enabled = true;
                replayCamera.GetComponent<AudioListener>().enabled = true;
            }
        }
        #endregion
    }
}
