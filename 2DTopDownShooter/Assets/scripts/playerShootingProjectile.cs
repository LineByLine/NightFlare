using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* HOW TO USE THIS SCRIPT:
It doesn't actually matter which GameObject in the scene you attach this to.
Just make sure you set the firingPoint to the playerBall's transform,
and set the bulletObject to some projectile. Prefabs work nicely for bulletObject.
 */
public class playerShootingProjectile : MonoBehaviour {
	[Tooltip("GameObject to instantiate when firing")]	
	public GameObject bulletObject;
	[Tooltip("Where the bullet will travel/check from (realistic gun)")]	
	public Transform firingPoint;
	private weaponInfoSegC useClip; //the weapon info script to access variables
	[Tooltip("Interval in between firing subsequent bullets.")]	
	public float fireCooldown;
	private float currentFireCooldown;

    public AudioClip shootingSoundEffect; //Shooting sound effect
    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>(); // Shooting sound effect

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
		if (currentFireCooldown >= fireCooldown)//you can fire if u have ammo in your clip and fireCooldown has worn off
		{
			//Spawn bullet
			GameObject.Instantiate(bulletObject, firingPoint.position,  gameObject.transform.rotation);
			//Start FireCooldown
			currentFireCooldown = 0;

            source.PlayOneShot(shootingSoundEffect);//Shooting sound effect
        }
	}
}
