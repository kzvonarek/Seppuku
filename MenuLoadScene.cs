using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoadScene : MonoBehaviour
{
    public void loadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void loadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void loadHelp()
    {
        SceneManager.LoadScene("Menu Help");
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("Menu Credits");
    }

    public void loadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
