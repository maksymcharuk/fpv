using UnityEngine;

namespace FPVDrone
{
    public class BaseDroneInput : MonoBehaviour
    {
        #region Variables
        [Header("Basic Input Properties")]
        protected float vertical = 0f;
        protected float horizontal = 0f;
        #endregion

        #region Built-in Methods
        void Update()
        {
            HandleInputs();
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleInputs()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
        #endregion
    }
}
