using UnityEngine;

public class Ability_Impulse : MonoBehaviour, IAbilityController
{
	float impulse_KnockBackForce = 10;
	float impulse_KnockbackRadius = 5;

	public void AbilityUse(Vector3 playerPosition)
	{
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
