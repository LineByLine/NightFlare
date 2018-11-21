using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void gotoMainMenu()
    {
        Debug.Log("Main Menu Button does something");
        SceneManager.LoadScene("MainMenuScene");
    }
}
