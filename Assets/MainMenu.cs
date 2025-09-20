using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject startPage;
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private string scene = "Main";

    public void ToCreditsPage()
    {
        creditsPage.SetActive(true);
        startPage.SetActive(false);
    }

    public void ToStartPage()
    {
        creditsPage.SetActive(false);
        startPage.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(scene);
    }
}
