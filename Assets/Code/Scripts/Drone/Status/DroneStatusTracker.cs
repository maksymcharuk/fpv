using UniRx;
using UnityEngine;

public class DroneStatusTracker : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    private CompositeDisposable subscriptions = new CompositeDisposable();
    private float groundSpeed = 0f;
    private Vector3 previousPosition;
    private float groundSpeedSmoothingFactor = 0.1f;

    #region Built-in Methods
    private void Start()
    {
        groundSpeed = 0f;
        previousPosition = transform.position;

        Observable.EveryUpdate().Subscribe(_ => UpdateStatus()).AddTo(subscriptions);
    }

    private void OnDestroy()
    {
        subscriptions.Clear();
    }
    #endregion

    #region Custom Methods
    private void UpdateStatus()
    {
        UpdateAltitude();
        UpdateGroundSpeed();
        UpdateAltitudeIndicator();
    }

    private void UpdateGroundSpeed()
    {
        // Update position
        Vector3 position = transform.position;

        // Calculate ground speed based on horizontal position change over time
        Vector3 horizontalMovement = new Vector3(
            position.x - previousPosition.x,
            0,
            position.z - previousPosition.z
        );
        float currentGroundSpeed = horizontalMovement.magnitude / Time.deltaTime;

        // Apply smoothing to ground speed
        groundSpeed = Mathf.Lerp(groundSpeed, currentGroundSpeed, groundSpeedSmoothingFactor);

        // Update previous position for next frame
        previousPosition = position;

        GameEvents.instance.groundSpeed.SetValueAndForceNotify(groundSpeed);
    }

    private void UpdateAltitude()
    {
        float altitude;

        // Calculate the altitude using raycasting
        if (
            Physics.Raycast(
                transform.position,
                Vector3.down,
                out RaycastHit hit,
                Mathf.Infinity,
                groundLayer
            )
        )
        {
            altitude = hit.distance;
        }
        else
        {
            // Fallback if no ground is detected (e.g., if flying over an empty area)
            altitude = transform.position.y;
        }

        GameEvents.instance.altitude.SetValueAndForceNotify(altitude);
    }

    private void UpdateAltitudeIndicator()
    {
        // Get the drone's pitch and roll
        float pitch = transform.eulerAngles.x;
        float roll = transform.eulerAngles.z;

        // Adjust the angles to be between -180 and 180 degrees
        if (pitch > 180)
            pitch -= 360;
        if (roll > 180)
            roll -= 360;

        GameEvents.instance.altitudeIndicatorRotation.SetValueAndForceNotify(
            Quaternion.Euler(-pitch, 0, -roll)
        );
    }
    #endregion
}
