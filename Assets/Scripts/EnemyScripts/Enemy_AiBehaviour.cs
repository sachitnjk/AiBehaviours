using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AiBehaviour : MonoBehaviour
{
	[SerializeField] Transform enemy_Target;
	[SerializeField] LayerMask playerMask;

	[SerializeField] private float enemy_SpeedWalk;
	[SerializeField] private float enemy_SpeedRun;

	[SerializeField] private float enemy_DetactionRange;
	[SerializeField] private float enemy_StartWaitTime;
	[SerializeField] private float enemy_TimeToRotate;

	float waitTime;
	float timeToRotate;

	[SerializeField] private float enemy_AttackRange;
	[SerializeField] private int enemy_Damage;
	[SerializeField] private float enemy_ViewRadius = 5f;
	[SerializeField] private float enemy_GapBetweenDamage;

	public NavMeshAgent navMeshAgent;

	private State enemy_CurrentState;

	public Enemy_AiWaypoints waypointsScript;
	private Transform[] waypoints;
	private	int enemy_CurrentWaypointIndex;

	private Vector3 player_LastKnownPos;

	private bool enemy_CanDamage;

	private enum State
	{
		Patrol,
		Chase,
		Searching,
		Attack,
		Dead
	}

	private void Start()
	{
		enemy_CanDamage = true;

		waypoints = waypointsScript.waypoints;

		enemy_CurrentWaypointIndex = 0;
		navMeshAgent = GetComponent<NavMeshAgent>();

		navMeshAgent.isStopped = false;
		navMeshAgent.speed = enemy_SpeedWalk;
		navMeshAgent.SetDestination(waypoints[enemy_CurrentWaypointIndex].position);


		enemy_CurrentState = State.Patrol;
	}

	private void Update()
	{
		switch (enemy_CurrentState)
			{
			case State.Patrol:
				Patrol();
				break;
			case State.Chase:
				Chasing();
				break;
			case State.Searching:
				Searching();
				break;
			case State.Attack:
				EnemyAttack(enemy_Damage);
				break;
			case State.Dead:
				EnemyDead();
				break;
		}

	}

	public void NextPoint()
	{
		enemy_CurrentWaypointIndex = (enemy_CurrentWaypointIndex + 1) % waypoints.Length;
		navMeshAgent.SetDestination(waypoints[enemy_CurrentWaypointIndex].position);
	}

	void Move(float speed)
	{
		navMeshAgent.isStopped = false;
		navMeshAgent.speed = speed;
	}

	void Stop()
	{
		navMeshAgent.isStopped = true;
		navMeshAgent.speed = 0;
	}

	private void Patrol()
	{
			navMeshAgent.SetDestination(waypoints[enemy_CurrentWaypointIndex].position);
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (waitTime <= 0)
				{
					NextPoint();
					Move(enemy_SpeedWalk);
					waitTime = enemy_StartWaitTime;
				}
				else
				{
					Stop();
					waitTime -= Time.deltaTime;
				}
			}

		Collider[] playerInRange = Physics.OverlapSphere(transform.position, enemy_ViewRadius, playerMask);

		if(playerInRange != null && playerInRange.Length > 0)
		{
			enemy_Target = playerInRange[0].transform;

			enemy_CurrentState = State.Chase;
		}

		//make coroutine of this later

	}

	private void Chasing()
	{
		Vector3 targetPosition = enemy_Target.position;
		var towardsPlayer = enemy_Target.position - transform.position;
		//player_LastKnownPos = enemy_Target.position;

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(towardsPlayer), Time.deltaTime * enemy_TimeToRotate);

		Move(enemy_SpeedRun);
		navMeshAgent.SetDestination(targetPosition);
		if (Vector3.Distance(transform.position, targetPosition) <= enemy_AttackRange)
		{
			enemy_CurrentState = State.Attack;
		}
		else if (Vector3.Distance(transform.position, targetPosition) > enemy_DetactionRange)
		{
			//search state
			//then do this maybe
			//enemy_CurrentState = State.Patrol;
			enemy_CurrentState = State.Searching;
		}
	}

	private void Searching()
	{
		player_LastKnownPos = enemy_Target.position;

		Debug.Log("going to player last pos");
		navMeshAgent.SetDestination(player_LastKnownPos);

		float distanceToPlayer = Vector3.Distance(transform.position, player_LastKnownPos);

		if (distanceToPlayer <= navMeshAgent.stoppingDistance && distanceToPlayer > enemy_DetactionRange)
		{
			Debug.Log("Searching -> patrol");
			enemy_CurrentState = State.Patrol;
		}
		else
		{
			Debug.Log("Searching -> Chase");
			enemy_CurrentState = State.Chase;
		}
	}

	void EnemyAttack(int damage)
	{
		if (enemy_CanDamage)
		{
			Debug.Log("I am attacking");
			//playerHealthBar.TakeDamage(damage);
			Stop();
			var towardsPlayer = enemy_Target.position - transform.position;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(towardsPlayer), Time.deltaTime * enemy_TimeToRotate);
			StartCoroutine(AiAttacked());
		}
	}

	private IEnumerator AiAttacked()
	{
		enemy_CanDamage = false;
		yield return new WaitForSeconds(enemy_GapBetweenDamage);
		enemy_CanDamage = true;

		if (Vector3.Distance(transform.position, enemy_Target.position) > enemy_AttackRange)
		{
			Debug.Log("attack -> chase");
			enemy_CurrentState = State.Chase;
		}
	}

	private void EnemyDead()
	{
		//trigger dead animation here
		//If cuurent health of enemy = 0,
		enemy_CurrentState = State.Dead;
		Debug.Log("EnemyDead");
	}

}
