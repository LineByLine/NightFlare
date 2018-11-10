using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponInfoSegC : MonoBehaviour {
	[Header("Gun Info")]
	/*Information about the gun's ammo counts; the total and clip amounts*/
	private int maxClip = 20; //how much ammo one clip can hold (information)
	public int currentClip; //current ammo in clip (mutable variable)
	private enum State {Normal, Reload};
	private State currentState;
	private float currentReloadProgress;
	public float reloadTime;

	// Use this for initialization
	void Start () {
		currentClip = maxClip;
		currentState = State.Normal;
		currentReloadProgress = 0;
	}

	void Update()
	{
		if(currentState == State.Reload)
		{
			currentReloadProgress += Time.deltaTime;	
		}
	}
	
	public void reload()
	{
		currentState = State.Reload;
		if(currentReloadProgress >= reloadTime)
		{
			currentClip = maxClip;
			currentReloadProgress = 0;
			currentState = State.Normal;
		}
	}
}
