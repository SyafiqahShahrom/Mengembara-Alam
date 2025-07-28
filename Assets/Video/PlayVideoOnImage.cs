using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class PlayVideoOnImage : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }
    }
}
