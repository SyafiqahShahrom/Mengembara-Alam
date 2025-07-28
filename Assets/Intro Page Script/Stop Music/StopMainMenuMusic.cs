using UnityEngine;

public class StopMainMenuMusic : MonoBehaviour
{
    void Start()
    {
        GameObject music = GameObject.FindGameObjectWithTag("PersistentMusic");

        if (music != null)
        {
            PersistentMusic fadeScript = music.GetComponent<PersistentMusic>();
            if (fadeScript != null)
            {
                fadeScript.StartFadeOutAndDestroy();
            }
            else
            {
                Destroy(music); // fallback just in case
            }
        }
    }
}
