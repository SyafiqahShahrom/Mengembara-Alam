using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARCameraController : MonoBehaviour 
{

    
    public void BackToMenu() 
    {
        PlayerPrefs.SetInt("ShowScene2Panel", 1);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("MainMenu");
    }
     public void GameButton()
    {
        SceneManager.LoadScene("MenuPermainan");
    }
    
}