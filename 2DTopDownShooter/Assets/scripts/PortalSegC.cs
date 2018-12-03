using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalSegC : MonoBehaviour {

	public string nextSceneName = "Map1.2";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBall"))
		{
			SceneManager.LoadScene("Map1.2");
		}
	}
}
