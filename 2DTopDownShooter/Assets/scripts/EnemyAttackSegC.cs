using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSegC : MonoBehaviour {
	public float damage;
	public float attackTime;
	private float attackProgress = 0;
	public float attackCooldownTime;
	private float currentAttackCooldownTime = 0;
	public float windupTime;
	private float currentWindup = 0;
	public Sprite defaultSprite;
    public Sprite attackSprite;
    private SpriteRenderer sr;

	public enum AttackState {Normal, Windup, Attacking, Cooldown};
	private AttackState currentAttackState = AttackState.Normal;

	private bool shouldAttack = false;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = defaultSprite;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentAttackState)
		{
			case AttackState.Normal:
			{
				break;
			}
			case AttackState.Windup:
			{
				if (currentWindup <= windupTime)
				{
					currentWindup += Time.deltaTime;
				}
				else
				{
					currentWindup = 0;
					currentAttackState = AttackState.Attacking;
					sr.sprite = attackSprite;
				}
				break;
			}
			case AttackState.Attacking:
			{
				if (attackProgress < attackTime)
				{
					attackProgress += Time.deltaTime;
				}
				else
				{
					sr.sprite = defaultSprite;
					attackProgress = 0;
					currentAttackState = AttackState.Cooldown;
					currentAttackCooldownTime = attackCooldownTime;
				}
				break;
			}
			case AttackState.Cooldown:
			{
				if(currentAttackCooldownTime > 0)
				{
					currentAttackCooldownTime -= Time.deltaTime;
				}
				else if(shouldAttack)
				{
					currentAttackState = AttackState.Windup;
				}
				else
				{
					currentAttackState = AttackState.Normal;
				}
				break;
			}
			default:
			{
				break;
			}
		}
	}

	void Attack(Collider2D other)
	{
		if(attackProgress == 0)
		{
			other.GetComponent<PlayerHealth>().TakeDamage(damage);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBall"))
			shouldAttack = false;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(currentAttackState == AttackState.Attacking && other.CompareTag("PlayerBall"))
		{
			Attack(other);
		}
		else if(currentAttackState == AttackState.Normal && other.CompareTag("PlayerBall"))
		{
			currentAttackState = AttackState.Windup;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("PlayerBall"))
			shouldAttack = false;
	}

	public float getAttackProgress()
	{
		return attackProgress;
	}

	public AttackState getCurrentAttackState()
	{
		return currentAttackState;
	}

	
}
