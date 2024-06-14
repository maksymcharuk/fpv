using UnityEngine;

namespace FPVDrone
{
    public class TankController : MonoBehaviour
    {
        #region Variables
        public Damageable turret;
        public Damageable body;
        private MovementController movementController = null;
        #endregion


        #region Built-in Methods
        private void Start()
        {
            movementController = GetComponent<MovementController>();
        }

        void Update()
        {
            if (movementController != null)
            {
                if (turret.Broken || body.Broken)
                {
                    movementController.ForceStop();
                }
            }
        }
        #endregion
    }
}
