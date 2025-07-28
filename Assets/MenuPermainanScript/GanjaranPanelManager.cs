using UnityEngine;
using UnityEngine.SceneManagement;

public class GanjaranPanelManager : MonoBehaviour
{
    public string backSceneName = "Scene2Panel"; // Change this to your intended back scene
    public string homeSceneName = "MainMenu";    // Change if your main menu is a different name

    public void BackButton()
    {
        SceneManager.LoadScene(backSceneName);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(homeSceneName);
    }
}
