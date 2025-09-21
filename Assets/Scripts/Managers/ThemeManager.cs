using UnityEngine;

public enum GameTheme
{
    Void, // all black
    Backrooms, // backrooms theme
    Happiness, // sunshine and rainbows and trees and grass
}

public class ThemeManager : MonoBehaviour
{
    public GameTheme currentTheme;
    public GameObject voidThemeParent;
    public GameObject backroomsThemeParent;
    public GameObject HappinessThemeParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetThemeGlobal(GameTheme.Void);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetThemeGlobal(GameTheme theme)
    {
        currentTheme = theme;
        Debug.Log("Set theme to " + currentTheme);
        changeTheme();
    }

    void changeTheme()
    {
        switch (currentTheme)
        {
            case GameTheme.Void:
                backroomsThemeParent.SetActive(false);
                HappinessThemeParent.SetActive(false);
                voidThemeParent.SetActive(true);
                break;
            case GameTheme.Backrooms:
                backroomsThemeParent.SetActive(true);
                HappinessThemeParent.SetActive(false);
                voidThemeParent.SetActive(false);
                break;
            case GameTheme.Happiness:
                backroomsThemeParent.SetActive(true);
                HappinessThemeParent.SetActive(true);
                voidThemeParent.SetActive(false);
                break;
        }
    }

}
