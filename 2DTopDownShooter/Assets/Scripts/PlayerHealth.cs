using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {

    public float hp;
    public float maxHp;
    public Image healthBar;
    public Text healthText;
    public GameObject GameOverContainer;

    float timestamp = 0.0f; //start for a timer for how long it has been since Player has been damaged

    public AudioClip takingDamageSound; //Damage sound effect
    private AudioSource source;



    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>(); // Damage sound effect

        healthText.text = (hp / maxHp * 100).ToString() + "%";
        healthBar.fillAmount = hp / maxHp;
        StartCoroutine(addHealth()); //health regeneration
    }

    // Update is called once per frame
    void Update () {
        if (hp <= 0) //What happens if Player dies
        {
            Death();
        }
    }
    public void TakeDamage(float damage)
    {
        timestamp = Time.time; //creates timestamp for after player takes damage
        hp -= damage;
        float currentHp = hp / maxHp;
        healthText.text = (currentHp * 100).ToString() + "%";
        healthBar.fillAmount = currentHp;

        source.PlayOneShot(takingDamageSound);//Shooting sound effect
    }
    public void Death()
    {
        isDead = true;
        GameObject player = GameObject.FindGameObjectWithTag("PlayerBall");
        foreach (MonoBehaviour m in player.GetComponents<MonoBehaviour>())
        {
            m.enabled = false;
        }

        //INSERT ATTACK/SHOOTING TO BE DISABLED
        source.volume = 0;
        GameOverContainer.SetActive(true);
        
    }

    IEnumerator addHealth() //Player recovers health up to 3/4 of maximum HP
    {
        while (true)
        {
            if (hp < 75 && Time.time > (timestamp + 3.0f) && !isDead) //only recovers if hp<75 and at least 3 seconds have passed and if player is not dead
            {
                hp += 1; //amount of hp that is being raised
                float currentHp = hp / maxHp;
                healthText.text = (currentHp * 100).ToString() + "%";
                healthBar.fillAmount = currentHp;
                yield return new WaitForSeconds(0.1f); //the rate at which hp is being raised. The smaller the number, the smoother the regen
            }
            else
            {
                yield return null;
            }
        }
        
    }
}
