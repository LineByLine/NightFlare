using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObscureEnemiesSegC : MonoBehaviour {

	public Text debugText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			debugText.text = "Enemy";
			other.gameObject.GetComponentInChildren<EnemyFadeOutSegC>().Reset();
		}
	}
}
