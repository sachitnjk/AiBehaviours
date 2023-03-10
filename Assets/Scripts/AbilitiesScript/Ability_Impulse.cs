using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Impulse : MonoBehaviour
{
	[SerializeField] float impulse_KnockBackForce;
	[SerializeField] float impulse_KnockbackRadius;

	StarterAssetsInputs _input;

	private void Start()
	{
		_input = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		if(_input.abilityUse)
		{
			AbilityUse();
		}
	}

	public void AbilityUse()
	{
		Vector3 playerPosition = transform.position;

		Collider[] colliders = Physics.OverlapSphere(playerPosition, impulse_KnockbackRadius);
		foreach(Collider collider in colliders)
		{
			Rigidbody rb = collider.GetComponent<Rigidbody>();
			if(rb != null)
			{
				Vector3 direction = rb.transform.position - playerPosition;
				direction.y = 0f;
				rb.AddForce(direction.normalized * impulse_KnockBackForce, ForceMode.Impulse);
			}
		}
	}
}
