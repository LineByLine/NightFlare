using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootingProjectile : MonoBehaviour {
	public GameObject bulletObject;
	public float fireCooldown;
	public float reloadTime;
	[Header("Shooting Number Variables")]
	public Transform firingPoint; //Where the bullet will travel/check from (realistic gun)
	private weaponInfo useClip; //the weapon info script to access variables
	private float currentFireCooldown;
	private float currentReloadProgress;
	// Use this for initialization
	void Start () {
		currentFireCooldown = fireCooldown;
		currentReloadProgress = 0;
        useClip = firingPoint.GetComponent<weaponInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1"))
		{
			fire();
		}
		else if(Input.GetButtonUp("Fire1"))//If we want to let the player mash the Fire button to fire faster
		{
			useClip.reload();
			currentFireCooldown = fireCooldown;
		}
	}

	void fire()
	{
		//if your current ammo is empty, we try to reload
		if (useClip.currentClip == 0 && currentReloadProgress >= reloadTime)
		{
			currentReloadProgress = 0;
			useClip.reload();
		}
		else if(useClip.currentClip == 0)
		{
			currentReloadProgress += Time.deltaTime;
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
		else//need to wait for cooldown
		{
			currentFireCooldown += Time.deltaTime;
		}
	}
}
