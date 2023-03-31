using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform[] playerSpawnPoints;

	private void Start()
	{
		int randomNumber = Random.Range(0, playerSpawnPoints.Length);
		Transform playerSpawnPoint = playerSpawnPoints[randomNumber];
		GameObject player_Instance = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
	}
}
