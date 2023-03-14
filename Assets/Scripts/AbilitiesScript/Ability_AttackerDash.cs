using StarterAssets;
using System.Collections;
using UnityEngine;

public class Ability_AttackerDash : MonoBehaviour, IAbilityController
{
	[SerializeField] private float attacker_dashDistance = 1f;
	[SerializeField] private float attacker_dashSpeed = 10f;

	StarterAssetsInputs _input;

	private void Start()
	{
		_input = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if (_input.abilityUse)
		{
			Vector3 playerPosition = transform.position;
			AbilityUse(playerPosition);
		}
	}


	public void AbilityUse(Vector3 playerPosition)
	{

		StartCoroutine(DashCoroutine(playerPosition));
	}

	IEnumerator DashCoroutine(Vector3 playerPosition)
	{
		Vector3 dashEndPoint = playerPosition + transform.forward * attacker_dashDistance;

		GetComponent<StarterAssetsInputs>().enabled = false;

		float elapsedCoroutineTime = 0f;
		while(elapsedCoroutineTime < attacker_dashDistance)
		{
			elapsedCoroutineTime += Time.deltaTime;
			float progress = elapsedCoroutineTime / attacker_dashDistance;
			Vector3 newPosition = Vector3.Lerp(playerPosition, dashEndPoint, progress);
			if(Time.deltaTime > 0)
			{
				transform.Translate((newPosition - playerPosition).normalized * attacker_dashDistance * attacker_dashSpeed * Time.deltaTime, Space.World);
			}

			yield return null;
		}
		GetComponent<StarterAssetsInputs>().enabled = true;
	}
}
