using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        Debug.Log("Start Game");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads next scene in queue
        SceneManager.LoadScene("RyanHScene");
    }
}
