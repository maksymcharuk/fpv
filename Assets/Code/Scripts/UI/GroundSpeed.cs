using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

namespace FPVDrone
{
    public class GroundSpeed : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private CompositeDisposable subscriptions = new CompositeDisposable();

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        #region Built-in Methods
        private void OnEnable()
        {
            StartCoroutine(Subscribe());
        }

        private void OnDisable()
        {
            subscriptions.Clear();
        }
        #endregion

        #region Custom Methods
        private IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => GameEvents.instance != null);
            GameEvents.instance.groundSpeed
                .ObserveEveryValueChanged(x => x.Value)
                .Subscribe(UpdateGroundSpeed)
                .AddTo(subscriptions);
        }

        private void UpdateGroundSpeed(float value)
        {
            text.text = "Ground Speed: " + value.ToString("F2") + "m/s";
        }
        #endregion
    }
}
