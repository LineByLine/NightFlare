using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform[] patrolAreas; //positions to move from
    private Transform playerTransform;
	private Transform target;

    public float speed;
    public float waitTime;
    public float startWaitTime;

    //States representing what type of movement Enemy should be executing right now
    public enum State {chase, attack, die};
    public State currentAction = State.chase;

    //Physics stuff relevant to movement
    private Rigidbody2D rb;

    [Header("Attack")]
    private float attackProgress = 0;
    public Sprite defaultSprite;
    public Sprite attackSprite;
    private SpriteRenderer sr;
    public float attackRange;
    public float attackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentsInChildren<SpriteRenderer>()[0];

        waitTime = startWaitTime;
		//Looks for the first GameObject tagged as "Player" and sets that GO's transform as the player attribute.
		//SO DON'T HAVE ANYTHING OTHER THAN THE PLAYER TAGGED AS "Player"
		playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		//At start, assume nothing is obstructing enemy from player so set player's transform as target
		target = playerTransform;
    }

    void Update()
    {
        switch(currentAction)
        {
            case State.chase:
            {
                Move();
                break;
            }
            case State.attack:
            {        
                ExecuteAttack();
                break;
            }
            case State.die:
            {
                break;
            }
            default:
                break;
        }
    }

    private void Move()
    {
        //Face the target node/player
        Look(target);
        //Move rigidbody and enemy position towards the target, smoothing any collisions with other objects.
        rb.MovePosition(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime));

        //Start attacking if player is close enough
        if(Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            currentAction = State.attack;
            sr.sprite = attackSprite;
        }
        
    }

    private void Look(Transform toLook)
    {
        /*This was given by the Scaffold.
        */
        Vector3 dir = toLook.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ExecuteAttack()
    {
        /* Zero Out Movement
        For some reason, the enemy will move oddly without these
        such as spiraling away from the player.
        This also makes sure that if another enemy is trying to shove its
        way to the player, the enemies that are being shoved won't spiral away
        from the player.*/
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        //Look(target);
        
        //Track how long the enemy has been attacking.
        //If it's longer than or equal the time it should
        //take to attack, stop and go back to chasing the player.
        attackProgress += Time.deltaTime;
        if(attackProgress >= attackTime)
        {
            attackProgress = 0;
            sr.sprite = defaultSprite;
            currentAction = State.chase;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
    }
}
