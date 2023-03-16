using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonShooter : MonoBehaviour
{

	[SerializeField] private CinemachineVirtualCamera cm_AimVirtualCamera;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask;
	[SerializeField] private Transform debugTransform;

	private ThirdPersonController _TPcontroller;
	private StarterAssetsInputs _input;

	private void Awake()
	{
		_input = GetComponent<StarterAssetsInputs>();
		_TPcontroller = GetComponent<ThirdPersonController>();
	}

	private void Update()
	{
		if(_input.aim)
		{
			cm_AimVirtualCamera.gameObject.SetActive(true);
			_TPcontroller.SetSensitivity(aimSensitivity);
		}
		else
		{
			cm_AimVirtualCamera.gameObject.SetActive(false);
			_TPcontroller.SetSensitivity(normalSensitivity);
		}

		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			debugTransform.position = raycastHit.point;
		}

	}

}
