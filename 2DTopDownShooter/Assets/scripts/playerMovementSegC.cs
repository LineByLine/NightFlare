using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementSegC : MonoBehaviour {

	[Header("Player Variables")]
	private Rigidbody2D rb;
	private GameObject mainCamera;
	public float playerX = 0; //player position x
	public float playerY = 0; //player position y
	public float moveSpeed = 15.0f; //speed of the player
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Zero out any velocity from collisions
		rb.velocity = Vector2.zero;

		MovePlayer();
		
		//Move camera to where the player is. Keep z the same or this the view will be weird.
		mainCamera.transform.position  = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
	}

	void MovePlayer()
	{
		//player moves up or down
		if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
		{
			playerY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
			rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + new Vector3(0.0f, playerY, 0.0f), moveSpeed * Time.deltaTime));
		}
		//player moves left or right
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
		{
			playerX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
			rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + new Vector3(playerX, 0.0f, 0.0f), moveSpeed * Time.deltaTime));
		}
	}
}
