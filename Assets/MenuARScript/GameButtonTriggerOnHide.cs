using UnityEngine;
using Vuforia;

public class GameButtonTriggerOnHide : MonoBehaviour
{
    public GameObject gameButton; // Butang yang kita nak paparkan

    private ObserverBehaviour observer;

    void Start()
    {
        gameButton.SetActive(false); // Sembunyikan butang bila mula

        observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void OnDestroy()
    {
        if (observer)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.NO_POSE)
        {
            // Target hilang dari kamera — butang ditunjukkan
            gameButton.SetActive(true);
        }
        else
        {
            // Target kelihatan — butang disembunyikan
            gameButton.SetActive(false);
        }
    }
}
