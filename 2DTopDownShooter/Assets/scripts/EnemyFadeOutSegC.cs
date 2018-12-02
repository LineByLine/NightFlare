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
		sr.color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(sr.color.a > 0f)
		{
			sr.color = new Color(1f, 1f, 1f, sr.color.a - Time.deltaTime / pulse_time);
		}
	}


	public void Reset()
	{
		sr.color = new Color(1f, 1f, 1f, 0.75f);
	}
		
}
