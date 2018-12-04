using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    public AudioClip buttonSound;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        Debug.Log("Start Game");
        source.PlayOneShot(buttonSound);
        SceneManager.LoadScene("TutorialSegC");
    }
}
