using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Decides what happens when buttons are clicked on the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{

    /// <summary>
    /// String of the name of the scene containing the level.
    /// that scene must be in the build settings.
    /// </summary>
    [Tooltip("Name of the scene of the level")]
    [SerializeField]
    private string levelName = "LevelTest";

    /// <summary>
    /// Empty containing the main menu buttons
    /// </summary>
    [SerializeField]
    private GameObject MainButtons;

    /// <summary>
    /// Empty containing the credits
    /// </summary>
    [SerializeField]
    private GameObject Credits;

    /// <summary>
    /// Play button object
    /// </summary>
    [SerializeField]
    private GameObject PlayButton;

    public void GoToLevel()
    {
        // play a sound
        PlayButton.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        
        // TODO: Async loading and scene fading
        SceneManager.LoadScene(levelName);
    }

    public void ShowCredits()
    {
        Credits.SetActive(true);
        MainButtons.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        Credits.SetActive(false);
        MainButtons.SetActive(true);
    }
}
