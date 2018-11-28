using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waves : MonoBehaviour {
    private int Wave = 0;
    public int WaveOneEnemies;
    public int WaveTwoEnemies;
    public int numberOfEnemies;
    public int enemyHealthOrigin;
    public int enemyDamageOrigin;
    public GameObject playerChar;
    public GameObject enemyType;
    public GameObject enemyDamageType;
    public Transform SpawnPoint;
    private GameObject[] NumEnemies;
    private PlayerHealth health;
    private EnemyAttackSegC enemyDamage;
    private enemyBehavior enemyHealth;
    private float playerHP;
    private bool EnemiesPresent;
	// Use this for initialization
	void Start () {
        EnemiesPresent = false;
        health = playerChar.GetComponent<PlayerHealth>();
        enemyDamage = enemyDamageType.GetComponent<EnemyAttackSegC>();
        enemyHealth = enemyType.GetComponent<enemyBehavior>();
        playerHP = health.maxHp;
        enemyHealth.startingHealth = enemyHealthOrigin;
        enemyDamage.damage = enemyDamageOrigin;
        NumEnemies = new GameObject[numberOfEnemies];
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Wave + " before");
        if(Wave > 2)
        {
            Debug.Log("YOU WIN");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        for (int i = 0; i < numberOfEnemies; i++) {
            if (NumEnemies[i] != null)
            {
                EnemiesPresent = true;
                break;
            }
            else
            {
                EnemiesPresent = false;
            }
            Debug.Log("During for Loop at: " + i + ": " + EnemiesPresent);
        }
        //Debug.Log(EnemiesPresent);
        if (!EnemiesPresent)
        {
            Wave++;
            SpawnEnemies();
        }
        //Debug.Log(Wave + " After");
    }
    public void SpawnEnemies()
    {
        if (Wave == 1)
        {
            for (int i = 0; i < WaveOneEnemies; i++)
            {
                NumEnemies[i] = Instantiate(enemyType, SpawnPoint.position, SpawnPoint.rotation);
            }
        Debug.Log("Spawning 1");
        }
        if (Wave == 2)
        {
            for (int i = 0; i < WaveTwoEnemies; i++)
            {
                NumEnemies[i] = Instantiate(enemyType, SpawnPoint.position, SpawnPoint.rotation);
                enemyHealth.startingHealth = 2 * enemyHealthOrigin;
                enemyDamage.damage = 2 * enemyDamageOrigin;
            }
            Debug.Log("Spawning 2");
        }
    }
}
