using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject optionsMenuGroup;
    public string gameStart;

    void Start()
    {
        mainMenuGroup.SetActive(true);
        optionsMenuGroup.SetActive(false);
    }

    public void StartNewGame()
    {
        //The below code may change later.
        SceneManager.LoadScene("Level");
    }

    public void LoadSavedGame()
    {
        mainMenuGroup.SetActive(false);
        optionsMenuGroup.SetActive(true);
    } 
    
    public void DisplayOptions()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        mainMenuGroup.SetActive(true);
        optionsMenuGroup.SetActive(false);
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
