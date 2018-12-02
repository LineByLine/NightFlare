using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSegC : MonoBehaviour {
	public float damage;
	public float attackTime;
	private float currentCounter = 0;
	public float attackCooldownTime;
	public float windupTime;
	public Sprite defaultSprite;
    public Sprite attackSprite;
	public Sprite windupSprite;
    private SpriteRenderer sr;

	public enum AttackState {Normal, Windup, Attacking, Cooldown};
	private AttackState currentAttackState = AttackState.Normal;

	private bool shouldAttack = false;
	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectsWithTag("PlayerBall")[0];
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = defaultSprite;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentAttackState)
		{
			case AttackState.Normal:
				if(shouldAttack)
				{
					currentAttackState = AttackState.Windup;
				}
				break;
			case AttackState.Windup:
				Windup();
				break;
			case AttackState.Attacking:
				Attack();
				break;
			case AttackState.Cooldown:
				Cooldown();
				break;
			default:
				break;
		}
	}

	void Attack()
	{
		if(currentCounter == 0)
		{
			player.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
		if (currentCounter < attackTime)
		{			
			currentCounter += Time.deltaTime;
		}
		else
		{
			TransitionAttackToCooldown();
		}
	}

	void Cooldown()
	{
		if(currentCounter < attackCooldownTime - windupTime)
		{
			currentCounter += Time.deltaTime;
		}
		else if(shouldAttack)
		{
			currentCounter = 0;
			currentAttackState = AttackState.Windup;
		}
		else
		{
			currentCounter = 0;
			currentAttackState = AttackState.Normal;
		}
	}

	void Windup()
	{
		if (currentCounter < windupTime)
		{
			currentCounter += Time.deltaTime;
		}
		else
		{
			currentCounter = 0;
			currentAttackState = AttackState.Attacking;
			sr.sprite = attackSprite;
		}
	}

	void TransitionAttackToCooldown()
	{
		currentAttackState = AttackState.Cooldown;
		sr.sprite = defaultSprite; //rest sprite
		currentCounter = 0;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag(player.tag) && !other.CompareTag("ReverseFlashlight"))
			shouldAttack = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag(player.tag) && !other.CompareTag("ReverseFlashlight"))
			shouldAttack = false;
	}

	public AttackState getCurrentAttackState()
	{
		return currentAttackState;
	}

	
}
