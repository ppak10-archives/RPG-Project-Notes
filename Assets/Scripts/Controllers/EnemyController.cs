using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;

	Transform target;
	NavMeshAgent agent;
	CharacterCombat combat;

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform; // sets the enemy's target and relavent data to player
		agent = GetComponent<NavMeshAgent>(); // sets agent to navmesh agent in component
		combat = GetComponent<CharacterCombat>(); // sets combat methods from character combat script
	}

	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(target.position, transform.position); // distance from player

		if (distance <= lookRadius) // if player is in enemy look radius
		{
			agent.SetDestination(target.position); // chase player

			if (distance <= agent.stoppingDistance) // if target in stopplng distance radius of enemy
			{
				// Attack the target
				CharacterStats targetStats = target.GetComponent<CharacterStats>(); // gets player stats data
				if (targetStats != null) // if player's stats are not null (which should probably be never)
				{
					combat.Attack(targetStats); // carry out attack by calling CharacterCombat script
				}


				// Face the target
				FaceTarget();

			}
		}
	}
	
	// Rotate enemy to face player
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
