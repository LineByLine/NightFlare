using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {

    public float hp;
    public float maxHp;
    public Image healthBar;
    public Text healthText;

    float timestamp = 0.0f; //start for a timer for how long it has been since Player has been damaged

    playerMovement playerMovement; //reference to moving script
    playerRotation playerRotation; //reference to player rotation script
    //NEED REFERENCE TO ATTACK/SHOOTING SCRIPT

    bool isDead = false;

    // Use this for initialization
    void Start()
    {
        healthText.text = (hp / maxHp * 100).ToString() + "%";
        healthBar.fillAmount = hp / maxHp;
        StartCoroutine(addHealth()); //health regeneration
        playerMovement = GetComponent<playerMovement>();
        playerRotation = GetComponentInChildren <playerRotation>(); //why doesn't this work???, Player can still rotate after death...
        //INSERT GETCOMPONENT FOR ATTACK/SHOOTING

    }

    // Update is called once per frame
    void Update () {

    }
    public void TakeDamage(float damage)
    {
        timestamp = Time.time; //creates timestamp for after player takes damage
        hp -= damage;
        float currentHp = hp / maxHp;
        healthText.text = (currentHp * 100).ToString() + "%";
        healthBar.fillAmount = currentHp;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("AAAAA");
        if (other.gameObject.CompareTag("Enemy") && hp>0)
        {
            Debug.Log("I've been hurt");
            TakeDamage(5);

            if (hp <= 0) //What happens if Player dies
            {
                Death();
            }
        }

    }

    public void Death()
    {
        Debug.Log("DEATH");
        isDead = true;
        playerMovement.enabled = false;
        playerRotation.enabled = false;
        //INSERT ATTACK/SHOOTING TO BE DISABLED
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
