using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponInfoSegC : MonoBehaviour {
	//[Header("Gun Info")]
	/*Information about the gun's ammo counts; the total and clip amounts*/
	private enum State {Normal, Reload};
	private State currentState;
	private float currentReloadProgress;
	public float reloadTime;

	// Use this for initialization
	void Start () {
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
}
