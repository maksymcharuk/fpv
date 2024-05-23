using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPVDrone
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private List<DamageablePart> damageableParts = new List<DamageablePart>();
        #endregion

        #region Custom Methods
        public void ApplyDamage(
            float explosionForce,
            Vector3 explostionCenter,
            float explosionRadius
        )
        {
            foreach (var part in damageableParts)
            {
                part.ApplyDamage(explosionForce, explostionCenter, explosionRadius);
            }
        }

        public void AddPart(DamageablePart part)
        {
            damageableParts.Add(part);
        }

        public bool AllBroken()
        {
            return damageableParts.All(part => part.Broken);
        }
        #endregion
    }
}
