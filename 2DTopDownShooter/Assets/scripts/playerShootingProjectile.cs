using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootingProjectile : MonoBehaviour {
	public GameObject bulletObject;
	[Header("Shooting Number Variables")]
	public Transform firingPoint; //Where the bullet will travel/check from (realistic gun)
	private weaponInfoSegC useClip; //the weapon info script to access variables
	public float fireCooldown;
	private float currentFireCooldown;
	// Use this for initialization
	void Start () {
		currentFireCooldown = fireCooldown;
        useClip = firingPoint.GetComponent<weaponInfoSegC>();
	}
	
	// Update is called once per frame
	void Update () {
		currentFireCooldown += Time.deltaTime;
		if (Input.GetButton("Fire1"))
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
		else if (currentFireCooldown >= fireCooldown)//you can fire if u have ammo in your clip and fireCooldown has worn off
		{
			//Spawn bullet
			GameObject.Instantiate(bulletObject, gameObject.transform.position,  gameObject.transform.rotation);
			//fires one bullet, subtracting from the current ammo 
			useClip.currentClip--;
			//Start FireCooldown
			currentFireCooldown = 0;
		}
	}
}
