using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	public float speed;
	private enemyBehavior damaging; //the enemy script to access variables


	// Use this for initialization
	void Start () {
		Debug.Log("Locked and loaded");
		sr = gameObject.GetComponent<SpriteRenderer>();
		rb = gameObject.GetComponent<Rigidbody2D>();
		gameObject.transform.rotation = GameObject.FindGameObjectsWithTag("Player")[0].transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		//WHY DOES THIS WORK
		//I couldn't figure out how to add the correct velocity, so I added the current position to a vector
		//Why does this vector make it go in the direction I want?
		rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + new Vector3(speed * Time.deltaTime, 0.0f, 0.0f), speed * Time.deltaTime));
		gameObject.transform.Translate(new Vector2(speed * Time.deltaTime, 0.0f));
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		string tag = collision.gameObject.tag;
		if (tag != "Player")
		{
			if (tag == "Enemy")
			{
				damaging = collision.gameObject.GetComponent<enemyBehavior>();
				damaging.currentHealth--;
			}
			//Kill this object if it hits anything that isn't a Player
			Destroy(gameObject);
		}
	}
}
