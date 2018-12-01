using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* HOW TO USE THIS SCRIPT
Attach this to the component of the enemy with the sprite renderer.
 */
public class EnemyPresenceSegC : MonoBehaviour {
	[Tooltip("How close this object has to be to the player to start the pulse animation")]
	public float pulse_range;
	[Tooltip("Duration (in seconds) of the fade-in/out pulse animation")]
	public float pulse_time;
	private float effective_pulse_time;
	[Tooltip("Aproximate threshold for how nonopaque this sprite can be before fading. Range: [0,1]")]
	public float max_alpha = 0.25f;
	//Should I be getting fainter or more visible?
	bool fading = false;
	//Whether or not to even change opacity
	private bool should_pulse = true;

	private SpriteRenderer sr;
	private Transform player;
	// Use this for initialization
	void Start () {
		max_alpha = Mathf.Clamp(max_alpha, 0f, 1f);
		effective_pulse_time = pulse_time / max_alpha;
		sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectsWithTag("PlayerBall")[0].transform;
		sr.color = new Color(1f, 1f, 1f, 0f);

	}
	
	// Update is called once per frame
	void Update () {
		if(should_pulse && Vector3.Distance(transform.position, player.position) <= pulse_range)
			Pulse();
		else if (should_pulse && sr.color.a != 0f)
			sr.color = new Color(1f, 1f, 1f, 0f);
	}

	private void Pulse()
	{
		sr.color = new Color(1f, 1f, 1f, fading ? sr.color.a - Time.deltaTime / effective_pulse_time: sr.color.a + Time.deltaTime / effective_pulse_time);
		if(sr.color.a < 0 || sr.color.a > max_alpha)
		{
			//fading = !fading;
		}
	}

	// Disable the behaviour when it becomes invisible...
    void OnBecameInvisible()
    {
        should_pulse = false;
		sr.color = new Color(1f, 1f, 1f, max_alpha);
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        should_pulse = true;
		sr.color = new Color(1f, 1f, 1f, max_alpha);
		fading = true;
    }
}
