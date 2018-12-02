using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The only reason this inherits from enemyBehavior is because I don't want to ask everyone to change any references to enemyBehavior
//Most of this was copied from Ryan's PlayerHealth script then reorganized.
/*HOW TO USE THIS SCRIPT:
 */
public class HealthSegC : enemyBehavior {
    public float maxHp;
	protected float hp;
	[Tooltip("Sound to play when this gameObject takes damage")]
    public AudioClip takingDamageSound; //Damage sound effect
    protected AudioSource source;
    bool isDead = false;

    // Use this for initialization
    void Start()
    {
		StartProcedure();
    }
	//What to call in Start for children, since Start isn't accessible
	protected void StartProcedure()
	{
		hp = maxHp;
        source = GetComponent<AudioSource>(); // Damage sound effect
		updateHealthUI();
	}

	public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        updateHealthUI();
		if(source)
			source.PlayOneShot(takingDamageSound);//Shooting sound effect
		if (hp <= 0) //What happens if gameObject dies
        {
            Death();
        }
    }

    public void Death()
    {
		Destroy(gameObject);
    }

	protected virtual void updateHealthUI() {}

	/*==========*//* Getters *//*==========*/
	public float getCurrentHealth()
	{
		return hp;
	}

	public bool getIsDead()
	{
		return isDead;
	}

}
