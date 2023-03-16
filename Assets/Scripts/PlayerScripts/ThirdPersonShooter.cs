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
		Vector3 mouseWorldPosition = Vector3.zero;

		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}

		if(_input.aim)
		{
			cm_AimVirtualCamera.gameObject.SetActive(true);
			_TPcontroller.SetSensitivity(aimSensitivity);
			_TPcontroller.SetRotateOnMove(false);

			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
		}
		else
		{
			cm_AimVirtualCamera.gameObject.SetActive(false);
			_TPcontroller.SetSensitivity(normalSensitivity);
			_TPcontroller.SetRotateOnMove(true);
		}


	}

}
