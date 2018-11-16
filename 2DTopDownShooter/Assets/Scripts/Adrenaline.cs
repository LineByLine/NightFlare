using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adrenaline : MonoBehaviour {
    public float originMoveSpeed;
    public bool canRun = true;
    public float newMoveSpeed = 20f;
    public float sp;
    private float originSP;
    private float newSP;
    private float originDelTime;
    private float delayTimer = 3f;
    public float moveSpeed = 10f;
    public float playerY = 0f;
    public float playerX = 0f;
    public Image spBar;
    public Text spText;

	// Use this for initialization
	void Start () {
        originSP = sp;
        originDelTime = delayTimer;
        originMoveSpeed = moveSpeed;
        spText.text = sp + " / " + originSP;
        spBar.fillAmount = sp / originSP;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            playerY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0.0f, playerY, 0.0f));
        }
        //player moves left or right
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            playerX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(playerX, 0.0f, 0.0f));
        }
        AdrenalineRush();
        newSP = Mathf.Round(sp);
        spBar.fillAmount = (newSP / originSP);
    }


    public void AdrenalineRush()
    {
        //
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
            moveSpeed = originMoveSpeed;
        }
    }
    //increases movement speed and depletes the bar within 3 seconds
    public void Run()
    {
        moveSpeed = newMoveSpeed;
        sp -= (originSP/3) * Time.deltaTime;
        newSP = Mathf.Round(sp);
        spText.text = newSP + " / " + originSP;
        spBar.fillAmount = (newSP / originSP);
    }
    //regenerates the bar within 3 seconds
    public void AdrenalineRegen()
    {
        sp += (originSP / 3) * Time.deltaTime;
        newSP = Mathf.Round(sp);
        spText.text = newSP + " / " + originSP;
    }
}
