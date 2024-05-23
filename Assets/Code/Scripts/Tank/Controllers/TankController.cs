using UnityEngine;

namespace FPVDrone
{
    public class TankController : EnemyController
    {
        #region Variables
        public DamageablePart cannon;
        public DamageablePart body;
        #endregion

        #region Built-in Methods
        public override void Start()
        {
            base.Start();

            damageable.AddPart(cannon);
            damageable.AddPart(body);
        }
        #endregion
    }
}
