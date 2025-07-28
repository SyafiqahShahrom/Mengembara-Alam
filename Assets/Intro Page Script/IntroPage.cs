using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPage : MonoBehaviour
{
    public void StartGame()
    {
        // Load the next scene 
        SceneManager.LoadScene("MainMenu"); 
    }
}
