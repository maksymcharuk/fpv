using UnityEngine;

namespace FPVDrone
{
    public abstract class Singletone<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public static T instance { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            instance = this as T;
        }
    }
}
