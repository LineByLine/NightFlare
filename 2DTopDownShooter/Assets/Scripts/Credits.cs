using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

    public GameObject MainMenuContainer;
    public GameObject CreditsContainer;
    public AudioClip buttonSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();


    }
    public void goToCredits()
    {
        //source.PlayOneShot(buttonSound);
        MainMenuContainer.SetActive(false);
        CreditsContainer.SetActive(true);
    }
}
