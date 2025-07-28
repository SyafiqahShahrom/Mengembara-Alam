using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuModulController : MonoBehaviour
{
    public void LoadModul1() => SceneManager.LoadScene("Pembakaran");
    public void LoadModul2() => SceneManager.LoadScene("PencemaranAsapKilang");
    public void LoadModul3() => SceneManager.LoadScene("PencairanAisKutub");
    public void LoadModul4() => SceneManager.LoadScene("Permulaan");
    public void LoadModul5() => SceneManager.LoadScene("Pengakhiran");

    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");


}
