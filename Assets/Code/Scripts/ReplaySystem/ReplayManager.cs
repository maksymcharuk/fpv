using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

namespace FPVDrone
{
    public class ReplayManager : Singletone<ReplayManager>
    {
        public RawImage screenRenderer; // Use RawImage for UI display
        public float playbackFrameRate = 30f;

        public GameObject gameplayCanvas; // Reference to the gameplay canvas
        public GameObject replayCanvas; // Reference to the replay canvas
        public Camera fallbackCamera; // Reference to the fallback camera

        private bool isReplaying = false;
        private List<Texture2D> replayFrames;
        private CompositeDisposable subscriptions = new CompositeDisposable();

        #region Built-in Methods
        private void OnDisable()
        {
            subscriptions.Clear();
        }
        #endregion

        public void StartReplay()
        {
            if (!isReplaying)
            {
                fallbackCamera.enabled = true;
                Observable
                    .Timer(TimeSpan.FromSeconds(2))
                    .Subscribe(_ =>
                    {
                        FrameCapture.instance.StopRecording();
                        replayFrames = FrameCapture.instance.GetFrames();
                        SwitchToReplayCanvas();
                        ReplayFrames().Subscribe();
                    })
                    .AddTo(subscriptions);
            }
        }

        private IObservable<Unit> ReplayFrames()
        {
            return Observable.FromCoroutine<Unit>(observer => ReplayCoroutine(observer));
        }

        private IEnumerator ReplayCoroutine(IObserver<Unit> observer)
        {
            isReplaying = true;
            float frameTime = 1f / playbackFrameRate;

            foreach (var frame in replayFrames)
            {
                screenRenderer.texture = frame;
                yield return new WaitForSeconds(frameTime);
            }

            isReplaying = false;
            observer.OnCompleted();
            SwitchToGameplayCanvas(); // Switch back to gameplay canvas after replay
        }

        private void SwitchToReplayCanvas()
        {
            gameplayCanvas.SetActive(false);
            replayCanvas.SetActive(true);
        }

        private void SwitchToGameplayCanvas()
        {
            replayCanvas.SetActive(false);
            gameplayCanvas.SetActive(true);
        }
    }
}
