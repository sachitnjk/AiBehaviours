using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemySpawnScript : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private Transform[] enemySpawnPoints;
	[SerializeField] private int EnemiesToSpawn;
	public Enemy_AiWaypoints waypointsScript;

	private void Start()
	{
		for(int i = 0; i < EnemiesToSpawn; i++)
		{
			int randomNumber = Random.Range(0, enemySpawnPoints.Length);
			Transform enemySpawnPoint = enemySpawnPoints[randomNumber];
			GameObject e_Instance = Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);

			/*e_Instance.GetComponent<EnemyHpController>().SetEnemyID(phView.ViewID);*/     //Setting view ID for spawned enemy for ownership transfer

			NavMeshAgent e_NavMeshAgent = e_Instance.GetComponent<NavMeshAgent>();
			e_NavMeshAgent.SetDestination(waypointsScript.waypoints[0].position);

			if (i == 0)
			{
				Enemy_AiBehaviour e_Leader = e_Instance.GetComponent<Enemy_AiBehaviour>();
				e_Leader.SetLeader(gameObject);
			}
		}
	}
}
