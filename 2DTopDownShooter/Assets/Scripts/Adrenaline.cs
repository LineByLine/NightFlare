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
        if (sp <= 0f)
        {
            canRun = false;
        }
        else
        {
            canRun = true;
        }
        if (Input.GetButton("Run") && canRun)
        {
            Run();
            delayTimer = originDelTime;
        }
        if (sp >= originSP)
        {
            delayTimer = originDelTime;
        }
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
    public void Run()
    {
        moveSpeed = newMoveSpeed;
        sp -= (originSP/3) * Time.deltaTime;
        newSP = Mathf.Round(sp);
        spText.text = newSP + " / " + originSP;
        spBar.fillAmount = (newSP / originSP);
    }
    public void AdrenalineRegen()
    {
        sp += (originSP / 3) * Time.deltaTime;
        newSP = Mathf.Round(sp);
        spText.text = newSP + " / " + originSP;
    }
}
