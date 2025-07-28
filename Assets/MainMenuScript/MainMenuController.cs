using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button HomeButton;
    public Button playScene1Button; // "BUKA" button
    public Button nextSceneButton;

    [Header("UI Panels")]
    public GameObject BGMainMenuPanel; // Panel 1 - Main Menu
    public GameObject Scene2Panel;     // Panel 2 - Second panel
    public GameObject Scene3Panel;     // Panel 3 - Third panel

    [Header("Scene Names")]
    public string introPageScene = "IntroPage";
    public string pembakaranScene = "Pembakaran";
    public string pencairanScene = "Pencairan";
    public string pencemaranScene = "Pencemaran";

    // Track current panel
    private int currentPanel = 1;

    private void Awake()
    {
        // Initialize panels FIRST - show only the main menu panel
        ShowPanel(1);
    }

    private void Start()
    {
        // Set up button listeners
        if (HomeButton != null)
            HomeButton.onClick.AddListener(GoToIntroPage);
        if (playScene1Button != null)
            playScene1Button.onClick.AddListener(GoToPembakaranScene);
        if (nextSceneButton != null)
            nextSceneButton.onClick.AddListener(GoToNextPanel);
    }

    /// <summary>
    /// Navigate to the IntroPage scene
    /// </summary>
    public void GoToIntroPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(introPageScene);
    }

    /// <summary>
    /// Navigate to the Pembakaran scene
    /// </summary>
    public void GoToPembakaranScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pembakaranScene);
    }

    /// <summary>
    /// Navigate to the next panel in sequence
    /// </summary>
    public void GoToNextPanel()
    {
        currentPanel++;
        if (currentPanel > 3) // If we go beyond panel 3, reset to panel 1
        {
            currentPanel = 1;
        }
        ShowPanel(currentPanel);
    }

    /// <summary>
    /// Navigate to the previous panel in sequence
    /// </summary>
    public void GoToPreviousPanel()
    {
        currentPanel--;
        if (currentPanel < 1) // If we go below panel 1, go to panel 3
        {
            currentPanel = 3;
        }
        ShowPanel(currentPanel);
    }

    /// <summary>
    /// Show specific panel and hide others
    /// </summary>
    private void ShowPanel(int panelNumber)
    {
        // Hide all panels first
        if (BGMainMenuPanel != null)
            BGMainMenuPanel.SetActive(false);
        if (Scene2Panel != null)
            Scene2Panel.SetActive(false);
        if (Scene3Panel != null)
            Scene3Panel.SetActive(false);

        // Show the requested panel
        switch (panelNumber)
        {
            case 1:
                if (BGMainMenuPanel != null)
                    BGMainMenuPanel.SetActive(true);
                break;
            case 2:
                if (Scene2Panel != null)
                    Scene2Panel.SetActive(true);
                break;
            case 3:
                if (Scene3Panel != null)
                    Scene3Panel.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Show the next scene panel (Panel 3)
    /// </summary>
    public void ShowNextScenePanel()
    {
        ShowPanel(3);
        currentPanel = 3;
    }

    /// <summary>
    /// Hide the next scene panel and show main menu
    /// </summary>
    public void HideNextScenePanel()
    {
        ShowPanel(1);
        currentPanel = 1;
    }

    /// <summary>
    /// Toggle the next scene panel visibility
    /// </summary>
    public void ToggleNextScenePanel()
    {
        if (Scene3Panel != null && Scene3Panel.activeSelf)
        {
            ShowPanel(1);
            currentPanel = 1;
        }
        else
        {
            ShowPanel(3);
            currentPanel = 3;
        }
    }

    private void OnDestroy()
    {
        // Clean up button listeners
        if (HomeButton != null)
            HomeButton.onClick.RemoveListener(GoToIntroPage);
        if (playScene1Button != null)
            playScene1Button.onClick.RemoveListener(GoToPembakaranScene);
        if (nextSceneButton != null)
            nextSceneButton.onClick.RemoveListener(GoToNextPanel);
    }
}