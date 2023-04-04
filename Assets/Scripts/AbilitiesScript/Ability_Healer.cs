using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Healer : MonoBehaviour, IAbilityController
{
	[SerializeField] private float healer_PlayerHealAmount;
	[SerializeField] private float healer_SelfHealAmount;

	public void AbilityUse(Vector3 playerPosition)
	{

	}
}
