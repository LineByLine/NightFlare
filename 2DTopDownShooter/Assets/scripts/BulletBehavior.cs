using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite impactSprite;
	private Rigidbody2D rb;
	public float speed;
	public float maxDistance;
	private enemyBehavior damaging; //the enemy script to access variables
	private float distTravelled;


	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();//In case we want this to change when it hits
		rb = gameObject.GetComponent<Rigidbody2D>();
		distTravelled = 0;
		
		//Ignore the collisions with the PlayerObject
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectsWithTag("PlayerBall")[0].GetComponent<Collider2D>());
		
		//Set rotation to match PlayerBall's.
		gameObject.transform.rotation = GameObject.FindGameObjectsWithTag("PlayerBall")[0].transform.rotation;
		rb.velocity = transform.right * speed * Time.deltaTime; //I don't know why we need to use transform.right
	}
	
	// Update is called once per frame
	void Update () {
		//Track how long this bullet has travelled. Delete it if this object has exceeded its maxDistance
		distTravelled += speed * Time.deltaTime;
		if(distTravelled >= maxDistance)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy") //Damage any enemy this touches
		{
			rb.simulated = false;
			sr.sprite = impactSprite;
			damaging = collision.gameObject.GetComponent<enemyBehavior>();
			damaging.currentHealth--;
			Destroy(gameObject, 0.5f);
		}
		else
		{
			Destroy(gameObject);
		}
		
	}
}
