using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{
    public GameObject mainMenuGroup;
    public GameObject optionsMenuGroup;

    void Start()
    {
        mainMenuGroup.SetActive(true);
        optionsMenuGroup.SetActive(false);
    }

    public void StartNewGame()
    {
        //The below code may change later.
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadSavedGame()
    {
        //More will be added to this later.
        Debug.Log("No saved games found.");
    }
    
    public void DisplayOptions()
    {
        mainMenuGroup.SetActive(false);
        optionsMenuGroup.SetActive(true);
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
}
