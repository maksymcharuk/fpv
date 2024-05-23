using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace FPVDrone
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(FixedJoint))]
    public class DamageablePart : MonoBehaviour
    {
        #region Variables
        public int health = 100;
        public FixedJoint joint;
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
            // rb.isKinematic = true;

            col = GetComponent<Collider>();
            joint = GetComponent<FixedJoint>();

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
                    joint.connectedBody = null;
                    // rb.isKinematic = false;

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
