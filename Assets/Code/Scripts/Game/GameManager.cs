using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FPVDrone
{
    public class GameManager : MonoBehaviour
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

            EnableReplayCamera();
            dronesLeft--;
            GameEvents.instance.dronesLeft.SetValueAndForceNotify(dronesLeft);

            if (dronesLeft <= 0)
            {
                Debug.Log("Game Over!");
                return;
            }

            if (spawnPoint != null)
            {
                Invoke("SpawnDrone", 3f);
            }
        }
        #endregion

        #region Custom Methods
        private void SpawnDrone()
        {
            if (dronePrefab != null && spawnPoint != null)
            {
                DisableReplayCamera();
                Instantiate(dronePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }

        private void EnableReplayCamera()
        {
            if (replayCamera != null)
            {
                replayCamera.enabled = true;
                replayCamera.GetComponent<AudioListener>().enabled = true;
            }
        }

        private void DisableReplayCamera()
        {
            if (replayCamera != null)
            {
                replayCamera.enabled = false;
                replayCamera.GetComponent<AudioListener>().enabled = false;
            }
        }
        #endregion
    }
}
