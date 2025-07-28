using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalItems = 3; // Set this to the number of rubbish items
    private int sortedItems = 0;
    public GameObject tamatPanel; // assign your well-done panel in Inspector

    public void CorrectItemSorted()
    {
        sortedItems++;

        if (sortedItems >= totalItems)
        {
            tamatPanel.SetActive(true); // show tamat screen
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }
}
