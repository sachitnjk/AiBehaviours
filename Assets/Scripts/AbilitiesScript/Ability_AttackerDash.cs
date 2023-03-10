using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Ability_AttackerDash : MonoBehaviour
{
	[SerializeField] float attacker_dashDistance;
	[SerializeField] float attacker_dashSpeed;

	float traveledDistance = 0f;

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
		Debug.Log("Dash is being called");
		Vector3 dashDirection = transform.forward;
		Vector3 forwardCamera = Camera.main.transform.forward;

		//removing y component from camera
		forwardCamera.y = 0f;
		forwardCamera.Normalize();

		if(traveledDistance < attacker_dashDistance)
		{
			float distancePerFrame = attacker_dashSpeed * Time.deltaTime;
			transform.position += forwardCamera * distancePerFrame;
			traveledDistance += distancePerFrame;
		}

	}
}
