using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseJosh : MonoBehaviour {
    private bool gamePaused = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("p"))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
	}
    private void Resume()
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }
    private void Paused()
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
