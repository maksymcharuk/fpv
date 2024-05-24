using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace FPVDrone
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Damageable : MonoBehaviour
    {
        #region Variables
        public int health = 100;
        public GameObject particles;

        private Rigidbody rb;
        private Collider col;
        #endregion

        #region Properties
        private bool broken = false;
        public bool Broken
        {
            get { return broken; }
        }
        #endregion

        #region Built-in Methods
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            col = GetComponent<Collider>();

            particles = GetComponentInChildren<ParticleSystem>()?.gameObject;
            if (particles)
            {
                particles.SetActive(false);
            }
        }
        #endregion

        #region Custom Methods
        public void ApplyDamage(
            float explosionForce,
            Vector3 explosionCenter,
            float explosionRadius
        )
        {
            float distance = Vector3.Distance(col.ClosestPoint(explosionCenter), explosionCenter);
            if (distance < explosionRadius)
            {
                var damage = (int)(explosionForce * (explosionRadius - distance));
                health -= damage;
                if (health <= 0)
                {
                    broken = true;

                    if (particles)
                    {
                        particles.SetActive(true);
                    }
                }
            }
        }
        #endregion
    }
}
