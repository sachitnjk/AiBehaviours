using StarterAssets;
using UnityEngine;


public interface IAbilityController
{
	void AbilityUse(Vector3 playerPosition);
}

public class AbilityController : MonoBehaviour
{
	private IAbilityController activeAbility;
	StarterAssetsInputs _input;

	private void Start()
	{
		_input = GetComponent<StarterAssetsInputs>();

		if(transform.gameObject.name == "PlayerImpulse-Char")
		{
			SetActiveAbility(new Ability_Impulse());
		}
		else if (transform.gameObject.name == "PlayerAttacker-Char")
		{
			SetActiveAbility(new Ability_AttackerDash());
		}
	}

	public void SetActiveAbility(IAbilityController ability)
	{
		activeAbility = ability;
	}

	private void Update()
	{
		if(_input.abilityUse)
		{
			Vector3 playerPosition = transform.position;
			activeAbility.AbilityUse(playerPosition);
		}
	}
}
