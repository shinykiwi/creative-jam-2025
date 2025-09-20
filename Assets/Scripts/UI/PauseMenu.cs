using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    
    public delegate void OnResume();
    public static OnResume onResume;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        Player.onPause += RequestPauseGame;
    }

    private void Start()
    {
        canvas.enabled = false;
    }

    private void OnDestroy()
    {
        Player.onPause -= RequestPauseGame;
    }

    private void RequestPauseGame()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
        }
    }
    public void OnResumeGame()
    {
        Toggle();
        
        onResume?.Invoke();
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnQuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Toggle()
    {
        canvas.enabled = !canvas.enabled;
    }
}
