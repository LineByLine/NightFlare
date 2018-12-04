using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMainMenu : MonoBehaviour {

    public GameObject MainMenuContainer;
    public GameObject CreditsContainer;
    public AudioClip buttonSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

    }
    public void returnToMenu()
    {
        //source.PlayOneShot(buttonSound);
        MainMenuContainer.SetActive(true);
        CreditsContainer.SetActive(false);

    }
}
