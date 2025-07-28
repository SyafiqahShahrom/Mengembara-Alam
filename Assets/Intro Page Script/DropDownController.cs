using UnityEngine;

public class DropDownController : MonoBehaviour
{
    public GameObject[] optionButtons; // drag Option1, Option2, etc. here in Inspector
    private bool isExpanded = false;

    public void ToggleDropdown()
    {
        isExpanded = !isExpanded;

        foreach (GameObject button in optionButtons)
        {
            button.SetActive(isExpanded);
        }
    }
}
