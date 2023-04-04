using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_TankDash : MonoBehaviour, IAbilityController
{
	[SerializeField] private float tanker_dashDistance = 1f;
	[SerializeField] private float tanker_dashSpeed = 10f;
	[SerializeField] private int tanker_DashDamage = 2;
	[SerializeField] private Vector3 tanker_EntityDamageChecker;
	[SerializeField] private LayerMask entityLayerMask;

	private float tanker_EntityDamageCheckerRadius = 5f;

	public void AbilityUse(Vector3 playerPosition)
	{
		StartCoroutine(TankerDashCoroutine(playerPosition));
		EntityDamageChecker();
	}

	IEnumerator TankerDashCoroutine(Vector3 playerPosition)
	{
		Vector3 dashEndPoint = playerPosition + transform.forward * tanker_dashDistance;

		GetComponent<StarterAssetsInputs>().enabled = false;

		float elapsedCoroutineTime = .8f;
		while (elapsedCoroutineTime < tanker_dashDistance)
		{
			elapsedCoroutineTime += Time.deltaTime;
			float progress = elapsedCoroutineTime / (tanker_dashDistance / 10);
			Vector3 newPosition = Vector3.Lerp(playerPosition, dashEndPoint, progress);
			if (Time.deltaTime > 0)
			{
				transform.Translate((newPosition - playerPosition).normalized * tanker_dashDistance * tanker_dashSpeed * Time.deltaTime, Space.World);
			}

			yield return null;
		}
		GetComponent<StarterAssetsInputs>().enabled = true;
	}

	private void EntityDamageChecker()
	{
		Collider[] entityInRange = Physics.OverlapSphere(tanker_EntityDamageChecker, tanker_EntityDamageCheckerRadius, entityLayerMask);
		if (entityInRange != null && entityInRange.Length > 0)
		{
			foreach(Collider enity in entityInRange)
			{
				//EnemyDamageTake(tanker_DashDamage);
				//Debug.Log(enemyHpController.e_CurrentHealth);
			}
		}
	}
}
