using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Load the main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Start game with button
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Load the end game menu
    public void LoadEndGame()
    {
        SceneManager.LoadScene(2);
    }
}
