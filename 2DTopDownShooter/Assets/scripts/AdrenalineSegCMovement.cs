using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* HOW TO USE THIS SCRIPT:
Attach this to the playerBall with a Rigidbody and Collider (NOT the playerManager).
Set Stamina UI sprites to images already on the Canvas.
Other than that, just set the public variables according to what they say in the tooltips.
 */

public class AdrenalineSegCMovement : MonoBehaviour {
    /*=================*/[Header("Stamina Values")]/*=================*/
    [Tooltip("Base moving speed when not running.")]
	public float normalSpeed = 10f;
	[Tooltip("Move speed when running.")]
    public float runSpeed = 20f;
    private float moveSpeed; //current move speed
	[Tooltip("Stamina. Initial value should be max stamina.")]
    public float sp;
    private bool canRun = true; //whether or not the player can run right now (has the stamina to or not)
    private float originSP; //max sp
    private float newSP; //current amt of sp (rounded) to display
    
    [Tooltip("How long until the stamina bar starts replenishing")]
    public float delayTimer = 3f;
    private float originDelTime; //Initial value for delay timer
    
    //Displacement along the Y and X axes for this frame
    private float playerY = 0f;
    private float playerX = 0f;
    
    /*=================*//*Stamina UI*//*=================*/
    //Image on the Canvas/Scene to display the current stamina.
    private Image spBar;
    //Text on the Canvas/Scene to display the current and max stamina.
    private Text spText;
	
    /*=================*//*Collision and Camera *//*=================*/
	//Rigidbody to handle collisions and stop movement
	private Rigidbody2D rb;
	//CameraObject to move with player
	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
        spBar = GameObject.FindGameObjectsWithTag("StamBar")[0].GetComponentInChildren<Image>();
        spText = GameObject.FindGameObjectsWithTag("StamText")[0].GetComponentInChildren<Text>();
        originSP = sp;
        originDelTime = delayTimer;
        moveSpeed = normalSpeed;
        updateStaminaUI();

		rb = GetComponent<Rigidbody2D>();
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Zero out any velocity from collisions
		rb.velocity = Vector2.zero;
        
        AdrenalineRush();
        updateStaminaUI();

		MovePlayer();
		//Move camera to where the player is. Keep z the same or this the view will be weird.
		mainCamera.transform.position  = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }
/*
    =====================From Christian's PlayerMovement==========================
*/
    /*
    Checks which Axes have input and offsets the player by that much.
    Uses rigidBody to handle collisions
    */
	void MovePlayer()
	{
		//player moves up or down
		if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
			playerY = Input.GetAxisRaw("Vertical");
		else
			playerY = 0f;

		//player moves left or right
		if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
			playerX = Input.GetAxisRaw("Horizontal");
		else
			playerX = 0f;
        
        //Put axis values into a Vector3. Normalize it for smoother diagonal movement.
        //Then multiply result by speed and time.
        Vector3 offset = (new Vector3(playerX, playerY, 0.0f)).normalized * moveSpeed * Time.deltaTime;
		rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + offset, moveSpeed * Time.deltaTime));
	}
/*
    =====================From Joshua's Adrenaline Script==========================
*/
    public void AdrenalineRush()
    {
        if (sp <= 0f)
        {
            canRun = false;
        }
        else
        {
            canRun = true;
        }
        //player runs when pressing the run button and can run (has stamina points). Resets the delay timer when the player runs
        if (Input.GetButton("Run") && canRun)
        {
            Run();
            delayTimer = originDelTime;
        }
        //reset the delay timer when the bar is full
        if (sp >= originSP)
        {
            delayTimer = originDelTime;
        }
        //if the player cannot run, then the bar regenerates after the delay time runs out. movement speed is set to its original speed.
        if (!Input.GetButton("Run") || !canRun)
        {
            if (sp < originSP)
            {
                if (delayTimer <= 0)
                {
                    AdrenalineRegen();
                }
                delayTimer -= Time.deltaTime;
            }
            moveSpeed = normalSpeed;
        }
    }
    //regenerates the bar within 3 seconds
    public void AdrenalineRegen()
    {
        sp += (originSP / 3) * Time.deltaTime;
        updateStaminaUI();
    }
    //increases movement speed and depletes the bar within 3 seconds
    public void Run()
    {
        moveSpeed = runSpeed;
        sp -= (originSP/3) * Time.deltaTime;
        updateStaminaUI();
    }

    void updateStaminaUI()
    {
        newSP = Mathf.Round(sp);
        spText.text = newSP + " / " + originSP;
        spBar.fillAmount = (newSP / originSP);
    }
}
