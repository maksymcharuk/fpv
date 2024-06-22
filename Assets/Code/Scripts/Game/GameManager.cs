using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FPVDrone
{
    public class GameManager : Singletone<GameManager>
    {
        #region Variables
        public Camera replayCamera;
        public GameObject dronePrefab;
        public Transform spawnPoint;

        private CompositeDisposable subscriptions = new CompositeDisposable();
        #endregion

        #region Built-in Methods
        void Awake()
        {
            if (replayCamera != null)
            {
                replayCamera.enabled = false;
                replayCamera.GetComponent<AudioListener>().enabled = false;
            }
        }

        void Start()
        {
            SpawnDrone();
        }

        private void OnEnable()
        {
            StartCoroutine(Subscribe());
        }

        private void OnDisable()
        {
            subscriptions.Clear();
        }
        #endregion

        #region Events
        private IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => GameEvents.instance != null);
            GameEvents.instance.droneDestroyed
                .WithLatestFrom(
                    GameEvents.instance.dronesLeft,
                    (destroyed, dronesLeft) => new { destroyed, dronesLeft }
                )
                .Subscribe(x => OnDroneDestroyed(x.destroyed, x.dronesLeft));
        }

        void OnDroneDestroyed(bool destroyed, int dronesLeft)
        {
            if (destroyed == false)
                return;

            ReplayManager.instance.StartReplay();

            dronesLeft--;
            GameEvents.instance.dronesLeft.SetValueAndForceNotify(dronesLeft);

            if (dronesLeft <= 0)
            {
                Debug.Log("Game Over!");
                return;
            }

            if (spawnPoint != null)
            {
                Invoke("SpawnDrone", 7f);
            }
        }
        #endregion

        #region Custom Methods
        private void SpawnDrone()
        {
            if (dronePrefab != null && spawnPoint != null)
            {
                GameObject drone = Instantiate(
                    dronePrefab,
                    spawnPoint.position,
                    spawnPoint.rotation
                );

                DroneController controller = drone.GetComponent<DroneController>();
                ReplayCamera camera =
                    FrameCapture.instance.captureCamera.GetComponent<ReplayCamera>();
                FallbackCamera fallbackCamera =
                    ReplayManager.instance.fallbackCamera.GetComponent<FallbackCamera>();

                ReplayManager.instance.fallbackCamera.enabled = false;
                camera.droneTransform = controller.transform;
                fallbackCamera.droneTransform = controller.transform;

                FrameCapture.instance.StartRecording();
            }
        }
        #endregion
    }
}
