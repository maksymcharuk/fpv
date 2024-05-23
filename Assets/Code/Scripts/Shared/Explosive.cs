using System.Linq;
using UnityEngine;

namespace FPVDrone
{
    public class Explosive : MonoBehaviour
    {
        #region Variables
        public float triggerForce = 0.5f;
        public float explosionRadius = 5f;
        public float explosionForce = 500f;
        public GameObject particles;
        #endregion

        #region Built-in Methods
        void OnCollisionEnter(Collision collision)
        {
            if (collision.relativeVelocity.magnitude >= triggerForce)
            {
                var surroundingObjects1 = Physics.OverlapSphere(
                    transform.position,
                    explosionRadius
                );

                foreach (var obj in surroundingObjects1)
                {
                    // Apply damage to damageable objects
                    Damageable damageable = obj.GetComponent<Damageable>();
                    if (damageable != null)
                    {
                        Debug.Log("Damageable: " + damageable);
                        damageable.ApplyDamage(explosionForce, transform.position, explosionRadius);
                    }
                }

                var surroundingObjects2 = Physics.OverlapSphere(
                    transform.position,
                    explosionRadius
                );

                foreach (var obj in surroundingObjects2)
                {
                    // Apply explosion force
                    var rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    }
                }

                if (particles)
                {
                    Instantiate(particles, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
        #endregion
    }
}
