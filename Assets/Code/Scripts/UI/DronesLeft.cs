using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;

public class DronesLeft : MonoBehaviour
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
        GameEvents.instance.dronesLeft
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(UpdateDronesLeft)
            .AddTo(subscriptions);
    }

    private void UpdateDronesLeft(int value)
    {
        text.text = $"Drones Left: {value}";
    }
    #endregion
}
