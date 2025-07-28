using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene2Manager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button HomeButton;
    public Button PlaySceneButton; // Button to go to Pencairan scene
    public Button NextSceneButton; // Button to go to next panel (Panel 3)
    public Button PreviousSceneButton;
    
    [Header("UI Panels")]
    public GameObject Scene2Panel; // Panel 2
    public GameObject NextScenePanel; // Panel 3
    public GameObject BGMainMenu; // Main menu panel that should be hidden when showing Scene2Panel
    
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
            PlaySceneButton.onClick.AddListener(GoToPlayScene);
        if (NextSceneButton != null)
            NextSceneButton.onClick.AddListener(GoToNextPanel);
        if (PreviousSceneButton != null)
            PreviousSceneButton.onClick.AddListener(GoToPreviousPanel);
        
        // Check if we're returning from Pencairan scene
        CheckForReturnFromPencairan();
        
        // Only set default panel states if we're NOT returning from Pencairan
        if (PlayerPrefs.GetInt("ShowScene2Panel", 0) == 0)
        {
            // Initially hide the next scene panel if it exists
            if (NextScenePanel != null)
                NextScenePanel.SetActive(false);
            
            // Show the main scene panel by default
            if (Scene2Panel != null)
                Scene2Panel.SetActive(true);
        }
    }
    
    /// <summary>
    /// Check if we're returning from Pencairan scene and show Scene2Panel accordingly
    /// </summary>
    private void CheckForReturnFromPencairan()
    {
        // Check if we need to show Scene2Panel (coming back from Pencairan)
        if (PlayerPrefs.GetInt("ShowScene2Panel", 0) == 1)
        {
            // Clear the flag
            PlayerPrefs.SetInt("ShowScene2Panel", 0);
            PlayerPrefs.Save();
            
            // Hide the main menu panel
            if (BGMainMenu != null)
                BGMainMenu.SetActive(false);
            
            // Make sure Scene2Panel is active and NextScenePanel is hidden
            if (Scene2Panel != null)
                Scene2Panel.SetActive(true);
            if (NextScenePanel != null)
                NextScenePanel.SetActive(false);
        }
        else
        {
            // Normal behavior - hide Scene2Panel and show main menu
            if (Scene2Panel != null)
                Scene2Panel.SetActive(false);
            if (NextScenePanel != null)
                NextScenePanel.SetActive(false);
            if (BGMainMenu != null)
                BGMainMenu.SetActive(true);
        }
    }
    
    /// <summary>
    /// Navigate to the IntroPage scene
    /// </summary>
    public void GoToIntroPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introPageScene);
    }
    
    /// <summary>
    /// Navigate to the play scene (Pencairan)
    /// </summary>
    public void GoToPlayScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pencairanScene);
    }
    
    /// <summary>
    /// Navigate to the next panel (Panel 3) - Fixed method
    /// </summary>
    public void GoToNextPanel()
    {
        if (Scene2Panel != null)
            Scene2Panel.SetActive(false);
        if (NextScenePanel != null)
            NextScenePanel.SetActive(true);
    }
    
    /// <summary>
    /// Navigate to the previous panel (hide current panel, show previous)
    /// </summary>
    public void GoToPreviousPanel()
    {
        if (NextScenePanel != null)
            NextScenePanel.SetActive(false);
        if (Scene2Panel != null)
            Scene2Panel.SetActive(true);
    }
    
    /// <summary>
    /// Show the next scene panel
    /// </summary>
    public void ShowNextScenePanel()
    {
        if (NextScenePanel != null)
            NextScenePanel.SetActive(true);
    }
    
    /// <summary>
    /// Hide the next scene panel
    /// </summary>
    public void HideNextScenePanel()
    {
        if (NextScenePanel != null)
            NextScenePanel.SetActive(false);
    }
    
    /// <summary>
    /// Toggle the next scene panel visibility
    /// </summary>
    public void ToggleNextScenePanel()
    {
        if (NextScenePanel != null)
            NextScenePanel.SetActive(!NextScenePanel.activeSelf);
    }
    
    private void OnDestroy()
    {
        // Clean up button listeners
        if (HomeButton != null)
            HomeButton.onClick.RemoveListener(GoToIntroPage);
        if (PlaySceneButton != null)
            PlaySceneButton.onClick.RemoveListener(GoToPlayScene);
        if (NextSceneButton != null)
            NextSceneButton.onClick.RemoveListener(GoToNextPanel);
        if (PreviousSceneButton != null)
            PreviousSceneButton.onClick.RemoveListener(GoToPreviousPanel);
    }
}