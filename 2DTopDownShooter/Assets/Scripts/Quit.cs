using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour {

    public AudioClip buttonSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void endTheGame()
    {
        source.PlayOneShot(buttonSound);
        Debug.Log("Quit game button works");
        Application.Quit();
    }
}
