using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* HOW TO USE THIS SCRIPT:
Assuming you have a collider on the Flashlight Shadow Mask, 
attach this script to the gameObject with the sprite you want to fade out under the mask.
That SpriteRenderer must be either Visible Under Mask or have No Interaction with masks.
 */
public class EnemyFadeOutSegC : MonoBehaviour {
	[Tooltip("Duration (in seconds) of the fade-out animation")]
	public float fade_time = 1f;
	[Tooltip("How solid the sprite should appear once it enters the mask. 0f = fully opaque. 1f = fully visible.")]
	public float base_alpha = 1f;
	//Need to divide deltaTime by this so the fade out takes as long as the specified duration
	private float alphaDecrementDivisor;
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		//Ensure given base_alpha is within proper alpha range
		base_alpha = Mathf.Clamp(base_alpha, 0f, 1f);
		
		sr = GetComponent<SpriteRenderer>();
		sr.color = new Color(1f, 1f, 1f, 0f);
		
		alphaDecrementDivisor = fade_time * base_alpha;
	}
	
	// Update is called once per frame
	void Update () {
		//Check alpha channel, which determines opacity. 
		//Don't need to do all these calculations if already max opacity.
		if(sr.color.a > 0f)
		{
			//Set new alpha: current alpha - time passed / how many seconds it should take to fade out * the starting nonopacity.
			sr.color = new Color(1f, 1f, 1f, sr.color.a - Time.deltaTime / alphaDecrementDivisor);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Reset opacity to the starting opacity when it goes under the shadow.
		if (other.gameObject.CompareTag("ReverseFlashlight"))
		{
			sr.color = new Color(1f, 1f, 1f, base_alpha);
		}
	}	
}
