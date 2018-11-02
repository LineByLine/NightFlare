using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform[] patrolAreas; //positions to move from
    private Transform playerTransform;
	private Transform target;

    public float speed;
    public float attackRange;
    public float waitTime;
    public float startWaitTime;

    public bool chasing;

    void Start()
    {
        waitTime = startWaitTime;
		//Looks for the first GameObject tagged as "Player" and sets that GO's transform as the player attribute.
		//SO DON'T HAVE ANYTHING OTHER THAN THE PLAYER TAGGED AS "Player"
		playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		//At start, assume nothing is obstructing enemy from player so set player's transform as target
		target = playerTransform;
    }

    void Update()
    {
        Look(target);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            //Debug.Log("attack");
        }
    }

    private void Look(Transform toLook)
    {
        Vector3 dir = toLook.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
    }
}
