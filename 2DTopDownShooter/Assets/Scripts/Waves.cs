using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {
    private int Wave = 1;
    public int NumEnemiesOne = 5;
    public int NumEnemiesTwo = 10;
    private GameObject[] NumEnemies;
    private GameObject enemy;
    private bool EnemiesPresent;
	// Use this for initialization
	void Start () {
        EnemiesPresent = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(NumEnemies.Length == 0)
        {
            EnemiesPresent = false;
            Wave++;
        }
        if(Wave > 2)
        {
            Debug.Log("YOU WIN");
        }
		if(!EnemiesPresent) {
            SpawnEnemies();
        }
        
	}
    private void SpawnEnemies()
    {
        if (Wave == 1) {
            for (int i = 0; i < NumEnemiesOne; i++)
            {

            }
            EnemiesPresent = true;
        }
        if(Wave == 2)
        {
            for(int i = 0;i < NumEnemiesTwo; i++)
            {

            }
        }
    }
}
