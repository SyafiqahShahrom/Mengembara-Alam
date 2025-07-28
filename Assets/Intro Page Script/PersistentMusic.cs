using UnityEngine;
using System.Collections;

public class PersistentMusic : MonoBehaviour
{
    public float fadeOutDuration = 2f; // seconds

    private AudioSource audioSource;
    private static bool isInitialized = false;

    void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("PersistentMusic");

        if (musicPlayers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            isInitialized = true;
        }
    }

    public void StartFadeOutAndDestroy()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        audioSource.Stop();
        Destroy(gameObject);
    }
}
