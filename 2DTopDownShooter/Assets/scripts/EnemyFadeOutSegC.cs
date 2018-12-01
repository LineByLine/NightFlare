using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFadeOutSegC : MonoBehaviour {
	[Tooltip("Duration (in seconds) of the fade-out animation")]
	public float pulse_time;
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sr.color.a > 0f)
		{
			sr.color = new Color(1f, 1f, 1f, sr.color.a - Time.deltaTime);
		}
	}


	// Disable the behaviour when it becomes invisible...
    void OnBecameInvisible()
    {
		//sr.color = new Color(1f, 1f, 1f, 1f);
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
		sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
