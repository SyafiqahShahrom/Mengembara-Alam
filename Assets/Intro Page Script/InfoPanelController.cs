using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoPanelController : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject tutorialPanel1;
    public GameObject tutorialPanel2;
    public GameObject tutorialPanel3;
    public GameObject logoutPanel;

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(false);
    }

    public void ShowTutorial1()
    {
        infoPanel.SetActive(false);
        tutorialPanel1.SetActive(true);
    }

    public void ShowTutorial2()
    {
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(true);
    }

    public void ShowTutorial3()
    {
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(true);
    }
public void ReturnToIntroPage()
{
    SceneManager.LoadScene("IntroPage"); // Make sure the scene name matches exactly
}

    public void HideAllPanels()
    {
        infoPanel.SetActive(false);
        tutorialPanel1.SetActive(false);
        tutorialPanel2.SetActive(false);
        tutorialPanel3.SetActive(false);
        logoutPanel.SetActive(false);
    }

    // 🔸 Called when logout button is clicked
    public void ShowLogoutPanel()
    {
        logoutPanel.SetActive(true);
    }

    // 🔸 YES button: Quit app
    public void ConfirmLogout()
    {
        Application.Quit(); // ✅ Works on Android/Build. Won't quit in Editor.
        Debug.Log("App closed."); // Just to confirm in Editor.
    }

    // 🔸 NO button: Cancel logout and return to IntroPage scene
    public void CancelLogout()
    {
        logoutPanel.SetActive(false);
        SceneManager.LoadScene("IntroPage"); // Replace with your actual scene name if different
    }
}
