using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBox : MonoBehaviour {
	private Text tutorialMessageHolder;
	 [TextArea(3,10)]
	public string tutorialMessage;

	// Use this for initialization
	void Start () {
		tutorialMessageHolder = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<Text>();
	}
	
	// Update is called once per frame

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBall"))
		{
			tutorialMessageHolder.text = tutorialMessage;
			Destroy(gameObject);
		}
	}	
}
