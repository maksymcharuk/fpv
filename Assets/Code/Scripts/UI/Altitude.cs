using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

namespace FPVDrone
{
    public class Altitude : MonoBehaviour
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
            GameEvents.instance.altitude
                .ObserveEveryValueChanged(x => x.Value)
                .Subscribe(UpdateAltitude)
                .AddTo(subscriptions);
        }

        private void UpdateAltitude(float value)
        {
            text.text = "Altitude: " + value.ToString("F2") + "m";
        }
        #endregion
    }
}
