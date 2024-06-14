using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPVDrone
{
    public class Path : MonoBehaviour
    {
        #region Variables
        private List<Transform> waypoints = new List<Transform>();

        private Color rayColor = Color.red;
        #endregion

        #region Properties
        public List<Transform> Waypoints
        {
            get { return waypoints; }
        }
        #endregion

        #region Built-in Methods
        private void OnDrawGizmos()
        {
            waypoints = GetComponentsInChildren<Transform>().Where(t => t != transform).ToList();

            Gizmos.color = rayColor;
            for (int i = 0; i < waypoints.Count; i++)
            {
                Vector3 position = waypoints[i].position;
                Gizmos.DrawWireSphere(position, 0.3f);
                if (i > 0)
                {
                    Vector3 previous = waypoints[i - 1].position;
                    Gizmos.DrawLine(previous, position);
                }
            }
        }
        #endregion
    }
}
