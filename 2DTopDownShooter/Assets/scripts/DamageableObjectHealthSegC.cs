using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObjectHealthSegC : HealthSegC {
	public Sprite damagedSprite = null;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		base.StartProcedure();
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void TakeDamage(float damage)
	{
		if(damagedSprite != null && sr.sprite != damagedSprite)
			sr.sprite = damagedSprite;
		base.TakeDamage(damage);
	}
}
