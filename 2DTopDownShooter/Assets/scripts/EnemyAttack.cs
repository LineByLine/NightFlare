using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag("Player")[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
