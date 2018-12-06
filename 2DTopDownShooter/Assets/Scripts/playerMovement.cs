using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* HOW TO USE THIS SCRIPT:
Attach this to the playerBall with a Rigidbody and Collider (NOT the playerManager).
Set Stamina UI sprites to images already on the Canvas.
Other than that, just set the public variables according to what they say in the tooltips.
 */

public class PlayerMovement : MonoBehaviour {
    /*=================*/[Header("Stamina Values")]/*=================*/
    [Tooltip("Base moving speed when not running.")]
	public float normalSpeed = 1f;
	[Tooltip("Move speed when running.")]
    public float runSpeed = 20f;
    [Tooltip("How long to wait for next movement input before not registering it as running.")]
    public float dashReleaseInterval = 0.75f;
    private float moveSpeed; //current move speed
    [Tooltip("Within how many degrees * 2 can the player ball's rotation be from the movement vector without being penalized.")]
    public float angleLenience;
    [Tooltip("What to reduce the player's speed to if they move in a direction they aren't facing.")]
    public float anglePenalty;
    private Vector3 oldOffset;
    private Vector3 lastMove;
	[Tooltip("Stamina. Initial value should be max stamina.")]
    public float sp;
    [Tooltip("How much stamina to regain per second.")]
    public float stamRegenSpd = 0.3f;
    private float originSP; //max sp
    
    [Tooltip("How long until the stamina bar starts replenishing")]
    public float staminaDelayTimer = 0.75f;
    private float originDelTime; //Initial value for delay timer
    
    //Displacement along the Y and X axes for this frame
    private float playerY = 0f;
    private float playerX = 0f;
    public float releaseTimer;
    
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
        oldOffset = Vector3.one;
        lastMove = Vector3.zero;
        
        releaseTimer = dashReleaseInterval;
        spBar = GameObject.FindGameObjectsWithTag("StamBar")[0].GetComponentInChildren<Image>();
        spText = GameObject.FindGameObjectsWithTag("StamText")[0].GetComponentInChildren<Text>();
        originSP = sp;
        originDelTime = staminaDelayTimer;
        staminaDelayTimer = 0;
        moveSpeed = normalSpeed;
        updateStaminaUI();

		rb = GetComponent<Rigidbody2D>();
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Zero out any velocity from collisions
		rb.velocity = Vector2.zero;
        //AdrenalineRush();
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
        updateStamina();
		SetPlayerXAndY();        
        
        Vector3 offset = Run();

        float movementAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; //conversion from radians to degrees
        
        if(!anglesAproximatelyEqual(transform.rotation.eulerAngles.z, movementAngle))
        {
            offset *= anglePenalty;
        }

        //Put axis values into a Vector3. Normalize it for smoother diagonal movement.
        //Then multiply result by speed and time.
		rb.MovePosition(Vector2.MoveTowards(transform.position, transform.position + offset, moveSpeed * Time.deltaTime));
	}

    private bool anglesAproximatelyEqual(float angle1, float baseAngle)
    {
        // https://gamedev.stackexchange.com/questions/4467/comparing-angles-and-working-out-the-difference
        float difference = 180 - Mathf.Abs(Mathf.Abs(angle1 - baseAngle) - 180); 
        return difference < angleLenience;
    }

    private Vector3 Run()
    {
        Vector3 offset = (new Vector3(playerX, playerY, 0.0f)).normalized;
        if (offset == Vector3.zero && oldOffset != Vector3.zero) //Just stopped
        {
            moveSpeed = normalSpeed;
            releaseTimer = 0;
        }
        //Moving in opposite direction
        else if (lastMove + offset == Vector3.zero)
        {
            moveSpeed = normalSpeed;
        }
        else if (offset != Vector3.zero && oldOffset == Vector3.zero && releaseTimer < dashReleaseInterval && sp > 0)
        {
            moveSpeed = runSpeed;
        }
        else if (moveSpeed == normalSpeed && releaseTimer >= dashReleaseInterval) //hasn't been running
        {
            releaseTimer += Time.deltaTime;
        }
        oldOffset = offset;
        if (offset != Vector3.zero)
            lastMove = offset;

        return offset.normalized * moveSpeed * Time.deltaTime;
    }

    private void SetPlayerXAndY()
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
    }

    private void updateStamina()
    {
        //Decrease stam if running
        if(moveSpeed == runSpeed && oldOffset != Vector3.zero)
        {
            sp -= Time.deltaTime;
            if(sp <= 0)
            {
                moveSpeed = normalSpeed;
                staminaDelayTimer = 0;
            }
        }
        else if (staminaDelayTimer >= originDelTime && sp < originSP)
        {
            sp += Time.deltaTime * stamRegenSpd;
        }
        else if (sp < originSP)
        {
            staminaDelayTimer += Time.deltaTime;
        }
    }

    void updateStaminaUI()
    {
        spText.text = Mathf.Round(sp) + " / " + originSP;
        spBar.fillAmount = (sp / originSP);
    }
}
