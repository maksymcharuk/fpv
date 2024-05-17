using System.Linq;
using UnityEngine;

namespace FPVDrone
{
    public class Breakable : MonoBehaviour
    {
        #region Variables
        public GameObject replacement;
        public float breakForce = 2f;
        public float collisionMultiplier = 100f;
        private bool broken = false;
        #endregion

        #region Built-in Methods
        void OnCollisionEnter(Collision collision)
        {
            if (broken)
                return;

            if (collision.relativeVelocity.magnitude >= breakForce)
            {
                broken = true;
                var r = Instantiate(replacement, transform.position, transform.rotation);

                var rbs = r.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                {
                    rb.AddExplosionForce(
                        collision.relativeVelocity.magnitude * collisionMultiplier,
                        collision.contacts[0].point,
                        2f
                    );
                }

                Destroy(gameObject);
            }
        }
        #endregion
    }
}
