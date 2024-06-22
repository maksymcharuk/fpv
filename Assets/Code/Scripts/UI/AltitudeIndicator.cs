using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

public class AltitudeIndicator : MonoBehaviour
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
        GameEvents.instance.altitudeIndicatorRotation
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(UpdateAltitudeIndicator)
            .AddTo(subscriptions);
    }

    private void UpdateAltitudeIndicator(Quaternion rotation)
    {
        text.transform.localRotation = rotation;
    }
    #endregion
}
