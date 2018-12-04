using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public AudioClip buttonSound;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void gotoMainMenu()
    {
        Debug.Log("Main Menu Button does something");
        SceneManager.LoadScene("MainMenuScene");
        source.PlayOneShot(buttonSound);
    }
}
