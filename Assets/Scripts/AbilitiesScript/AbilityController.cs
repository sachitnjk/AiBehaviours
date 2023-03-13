using StarterAssets;
using UnityEngine;


public interface IAbilityController
{
	void AbilityUse();
}

public class AbilityController : MonoBehaviour
{
	private IAbilityController activeAbility;
	StarterAssetsInputs _input;

	private void Start()
	{
		_input = GetComponent<StarterAssetsInputs>();

		if(transform.root.gameObject.name == "PlayerImpulse-Char")
		{
			SetActiveAbility(new Ability_Impulse());
		}
		else
		{
			SetActiveAbility(new Ability_Impulse());
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
			activeAbility.AbilityUse();
		}
	}
}
