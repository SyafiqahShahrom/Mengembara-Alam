using UnityEngine;
using UnityEngine.SceneManagement;

public class StopPersistentMusic : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Hanya stop muzik jika berada di "Intro" atau "MainMenu"
        if (currentScene == "Intro" || currentScene == "MainMenu")
        {
            GameObject music = GameObject.FindGameObjectWithTag("PersistentMusic");
            if (music != null)
            {
                Destroy(music);
            }
        }
    }
}
