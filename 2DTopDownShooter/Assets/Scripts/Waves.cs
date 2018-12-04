using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waves : MonoBehaviour {
    public int Wave = 0;
    public int WaveOneEnemies;
    public int WaveTwoEnemies;
    public int numberOfEnemies;
    public int enemyHealthOrigin;
    public int enemyDamageOrigin;
    private int otherNumEnemies;
    public GameObject playerChar;
    public GameObject enemyType;
    public GameObject enemyDamageType;
    public GameObject SpawnOne;
    public GameObject SpawnTwo;
    public GameObject SpawnThree;
    public Transform SpawnPoint;
    public GameObject[] NumEnemies;
    public GameObject[] OtherEnemies;
    public GameObject WaveOneCanvas;
    public GameObject WaveTwoCanvas;
    private Waves FirstSpawn;
    private Waves SecondSpawn;
    private Waves ThirdSpawn;
    private PlayerHealth health;
    private EnemyAttackSegC enemyDamage;
    private enemyBehavior enemyHealth;
    private IEnumerator spawn1;
    private IEnumerator spawn2;
    //private IEnumerator startwave;
    private float playerHP;
    private bool EnemiesPresent;
    // Use this for initialization
    void Start()
    {
        EnemiesPresent = false;
        health = playerChar.GetComponent<PlayerHealth>();
        enemyDamage = enemyDamageType.GetComponent<EnemyAttackSegC>();
        enemyHealth = enemyType.GetComponent<enemyBehavior>();
        playerHP = health.maxHp;
        enemyHealth.startingHealth = enemyHealthOrigin;
        enemyDamage.damage = enemyDamageOrigin;
        NumEnemies = new GameObject[numberOfEnemies];
        FirstSpawn = SpawnOne.GetComponent<Waves>();
        SecondSpawn = SpawnTwo.GetComponent<Waves>();
        ThirdSpawn = SpawnThree.GetComponent<Waves>();
        spawn1 = SpawnEnemies();
        spawn2 = SpawnEnemiesTwo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Wave > 3)
        {
            Debug.Log("YOU WIN");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (NumEnemies[i] != null)
            {
                EnemiesPresent = true;
                break;
            }
            else
            {
                EnemiesPresent = false;
            }
        }
        bool nextwave = !EnemiesPresent && !FirstSpawn.EnemiesPresent && !SecondSpawn.EnemiesPresent && !ThirdSpawn.EnemiesPresent;
        Debug.Log("Wave: " + Wave + " Spawn?: " + nextwave);
        if (nextwave)
        {
            Wave++;
            if (Wave == 1)
            {
                StartCoroutine(spawn1);
                StartCoroutine(WaveOneDisplay());
            }
            if (Wave == 2)
            {
                StartCoroutine(WaveTwoDisplay());
                StartCoroutine(spawn2);
            }
        }
    }
    private IEnumerator SpawnEnemies()
    {
        if (Wave == 1)
        {
            for (int i = 0; i < WaveOneEnemies; i++)
            {
                NumEnemies[i] = Instantiate(enemyType, SpawnPoint.position, SpawnPoint.rotation);
                yield return new WaitForSeconds(1);
            }
            Debug.Log("Spawning 1");
            StopCoroutine(spawn1);
        }
    }
    private IEnumerator SpawnEnemiesTwo()
    {
        for (int i = 0; i < WaveTwoEnemies; i++)
        {
            NumEnemies[i] = Instantiate(enemyType, SpawnPoint.position, SpawnPoint.rotation);
            enemyHealth.startingHealth = 2 * enemyHealthOrigin;
            enemyDamage.damage = 2 * enemyDamageOrigin;
            yield return new WaitForSeconds(1);
        }
        Debug.Log("Spawning 2");
        StopCoroutine(spawn2);
    }
    private IEnumerator WaveOneDisplay()
    {
        WaveOneCanvas.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(DeactivateWaveOneDisplay());
    }
    private IEnumerator WaveTwoDisplay()
    {
        WaveTwoCanvas.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(DeactivateWaveTwoDisplay());
    }
    private IEnumerator DeactivateWaveTwoDisplay(){
        yield return new WaitForSeconds(2);
        WaveTwoCanvas.SetActive(false);
    }
    private IEnumerator DeactivateWaveOneDisplay()
    {
        yield return new WaitForSeconds(2);
        WaveOneCanvas.SetActive(false);
    }
}
