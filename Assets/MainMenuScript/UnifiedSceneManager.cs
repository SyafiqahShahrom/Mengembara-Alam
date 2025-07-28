using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnifiedSceneManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button HomeButton;
    public Button PlaySceneButton;
    public Button NextSceneButton;
    public Button PreviousSceneButton;

    [Header("UI Panels")]
    public GameObject BGMainMenuPanel;  // Panel 1 - Main Menu
    public GameObject Scene2Panel;      // Panel 2
    public GameObject Scene3Panel;      // Panel 3

    [Header("Scene Names")]
    public string introPageScene = "IntroPage";
    public string pembakaranScene = "Pembakaran";
    public string pencairanScene = "Pencairan";
    public string pencemaranScene = "Pencemaran";

    // Track current panel
    private int currentPanel = 1;

    private void Awake()
    {
        // FORCE panels to correct state FIRST before anything else
        InitializePanels();
    }

    private void Start()
    {
        // Set up button listeners
        SetupButtonListeners();
        
        // Double-check panel state
        ShowPanel(1);
    }

    private void InitializePanels()
    {
        // Force hide all panels first
        if (BGMainMenuPanel != null) BGMainMenuPanel.SetActive(false);
        if (Scene2Panel != null) Scene2Panel.SetActive(false);
        if (Scene3Panel != null) Scene3Panel.SetActive(false);
        
        // Show only the main menu panel
        if (BGMainMenuPanel != null) BGMainMenuPanel.SetActive(true);
        
        currentPanel = 1;
    }

    private void SetupButtonListeners()
    {
        if (HomeButton != null)
            HomeButton.onClick.AddListener(GoToIntroPage);
        if (PlaySceneButton != null)
            PlaySceneButton.onClick.AddListener(HandlePlayButton);
        if (NextSceneButton != null)
            NextSceneButton.onClick.AddListener(GoToNextPanel);
        if (PreviousSceneButton != null)
            PreviousSceneButton.onClick.AddListener(GoToPreviousPanel);
    }

    public void GoToIntroPage()
    {
        SceneManager.LoadScene(introPageScene);
    }

    public void HandlePlayButton()
    {
        Debug.Log($"Play button pressed from Panel {currentPanel}");
        
        switch (currentPanel)
        {
            case 1:
                // From BGMainMenu panel, go to Pembakaran scene
                Debug.Log("Loading Pembakaran scene from BGMainMenu");
                SceneManager.LoadScene(pembakaranScene);
                break;
            case 2:
                // From Scene2Panel, go to Pencairan scene
                Debug.Log("Loading Pencairan scene from Scene2Panel");
                SceneManager.LoadScene(pencairanScene);
                break;
            case 3:
                // From Scene3Panel, go to Pencemaran scene
                Debug.Log("Loading Pencemaran scene from Scene3Panel");
                SceneManager.LoadScene(pencemaranScene);
                break;
            default:
                Debug.LogWarning($"Unknown panel {currentPanel}, defaulting to Pembakaran scene");
                SceneManager.LoadScene(pembakaranScene);
                break;
        }
    }

    public void GoToNextPanel()
    {
        if (currentPanel < 3)
        {
            currentPanel++;
            ShowPanel(currentPanel);
        }
        else
        {
            Debug.Log("Already at the last panel (Panel 3)");
        }
    }

    public void GoToPreviousPanel()
    {
        if (currentPanel > 1)
        {
            currentPanel--;
            ShowPanel(currentPanel);
        }
        else
        {
            Debug.Log("Already at the first panel (BGMainMenu)");
        }
    }

    private void ShowPanel(int panelNumber)
    {
        // Hide all panels first
        if (BGMainMenuPanel != null) BGMainMenuPanel.SetActive(false);
        if (Scene2Panel != null) Scene2Panel.SetActive(false);
        if (Scene3Panel != null) Scene3Panel.SetActive(false);

        // Show the requested panel
        switch (panelNumber)
        {
            case 1:
                if (BGMainMenuPanel != null) BGMainMenuPanel.SetActive(true);
                break;
            case 2:
                if (Scene2Panel != null) Scene2Panel.SetActive(true);
                break;
            case 3:
                if (Scene3Panel != null) Scene3Panel.SetActive(true);
                break;
        }
        
        currentPanel = panelNumber;
        Debug.Log($"Showing Panel {panelNumber}");
    }

    public void ShowSpecificPanel(int panelNumber)
    {
        ShowPanel(panelNumber);
    }

    private void OnDestroy()
    {
        // Clean up button listeners
        if (HomeButton != null) HomeButton.onClick.RemoveListener(GoToIntroPage);
        if (PlaySceneButton != null) PlaySceneButton.onClick.RemoveListener(HandlePlayButton);
        if (NextSceneButton != null) NextSceneButton.onClick.RemoveListener(GoToNextPanel);
        if (PreviousSceneButton != null) PreviousSceneButton.onClick.RemoveListener(GoToPreviousPanel);
    }
}