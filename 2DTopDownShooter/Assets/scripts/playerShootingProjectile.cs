using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootingProjectile : MonoBehaviour {
	public GameObject bulletObject;
	[Header("Shooting Number Variables")]
	public Transform firingPoint; //Where the bullet will travel/check from (realistic gun)
	private weaponInfo useClip; //the weapon info script to access variables

	// Use this for initialization
	void Start () {

        useClip = firingPoint.GetComponent<weaponInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			fire();
		}
	}

	void fire()
	{
		//if your current ammo is empty, we try to reload
		if (useClip.currentClip == 0)
		{
			useClip.reload();
		}
		else //you can fire if u have ammo in your clip
		{		
			//Spawn bullet
			GameObject.Instantiate(bulletObject, gameObject.transform.position,  gameObject.transform.rotation);
			

			//fires one bullet, subtracting from the current ammo 
			useClip.currentClip--;
		}
	}
}
