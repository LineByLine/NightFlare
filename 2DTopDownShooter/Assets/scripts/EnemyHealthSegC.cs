using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
HOW TO USE THIS SCRIPT:
Attach this script to the gameObject with the collider representing its hitbox.
Set the healthBar SpriteRenderer to whatever sprite renderer will display the healthBar.
 */
public class EnemyHealthSegC : HealthSegC {
	protected static Quaternion zeroQuaternion = new Quaternion(0, 0, 0, 1);

	[Tooltip("How long the Health Bar should stay up after being hit.")]
	public float barDisplayTime = 0.5f;
	//Keeping this private so outside methods don't mess with it.
	private float maxBarDisplayTime;
	[Tooltip("SpriteRenderer that displays the health bar")]
    public SpriteRenderer healthBar;

	// Use this for initialization
	void Start () {
		base.StartProcedure();
		healthBar.enabled = true;
		maxBarDisplayTime = barDisplayTime;
		barDisplayTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(barDisplayTime < maxBarDisplayTime)
		{
			barDisplayTime += Time.deltaTime;
			healthBar.enabled = barDisplayTime < maxBarDisplayTime;
		}
		//Make sure the healthBar hasn't been rotated by collisions.
		healthBar.gameObject.transform.rotation = zeroQuaternion;
	}
	
	public override void TakeDamage(float damage)
	{
		healthBar.enabled = true;
		barDisplayTime = 0;
		base.TakeDamage(damage);
	}

	protected override void updateHealthUI()
	{
		float currentHp = hp / maxHp;
		Transform barTransform = healthBar.gameObject.transform;
        healthBar.gameObject.transform.localScale = new Vector3(currentHp, barTransform.localScale.y, barTransform.localScale.z);
	}

}
