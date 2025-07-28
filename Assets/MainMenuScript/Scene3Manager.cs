using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene3Manager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button HomeButton;
    public Button PlaySceneButton; // Button to go to Pencemaran scene
    public Button PreviousSceneButton;
    
    [Header("UI Panels")]
    public GameObject Scene3Panel;
    
    [Header("Scene Names")]
    public string introPageScene = "IntroPage";
    public string pembakaranScene = "Pembakaran";
    public string pencairanScene = "Pencairan";
    public string pencemaranScene = "Pencemaran";
    
    private void Start()
    {
        // Set up button listeners
        if (HomeButton != null)
            HomeButton.onClick.AddListener(GoToIntroPage);
        
        if (PlaySceneButton != null)
            PlaySceneButton.onClick.AddListener(GoToNextScene);
        
        if (PreviousSceneButton != null)
            PreviousSceneButton.onClick.AddListener(GoToPreviousScene);
        
        // Show the main scene panel
        if (Scene3Panel != null)
            Scene3Panel.SetActive(true);
    }
    
    /// <summary>
    /// Navigate to the IntroPage scene
    /// </summary>
    public void GoToIntroPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introPageScene);
    }
    
    /// <summary>
    /// Navigate to the next scene (Pencemaran)
    /// </summary>
    public void GoToNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pencemaranScene);
    }
    
    /// <summary>
    /// Navigate to the previous scene (Pencairan)
    /// </summary>
    public void GoToPreviousScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pencairanScene);
    }
    
    private void OnDestroy()
    {
        // Clean up button listeners
        if (HomeButton != null)
            HomeButton.onClick.RemoveListener(GoToIntroPage);
        
        if (PlaySceneButton != null)
            PlaySceneButton.onClick.RemoveListener(GoToNextScene);
        
        if (PreviousSceneButton != null)
            PreviousSceneButton.onClick.RemoveListener(GoToPreviousScene);
    }
}