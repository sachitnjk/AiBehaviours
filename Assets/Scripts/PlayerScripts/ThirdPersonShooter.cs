using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooter : MonoBehaviour
{

	[SerializeField] private CinemachineVirtualCamera cm_AimVirtualCamera;
	[SerializeField] private float normalSensitivity;
	[SerializeField] private float aimSensitivity;
	[SerializeField] private LayerMask aimColliderLayerMask;
	[SerializeField] private Transform pf_BulletProjectile;	
	[SerializeField] private Transform bulletSpawnPoint;
	[SerializeField] private GameObject aimGameObject;

	private ThirdPersonController _TPcontroller;
	private StarterAssetsInputs _input;
	private Animator animator;

	private PlayerInput _playerInput;
	private InputAction shootAction;

	private void Awake()
	{
		_input = GetComponent<StarterAssetsInputs>();
		_TPcontroller = GetComponent<ThirdPersonController>();
		_playerInput = GetComponent<PlayerInput>();
		shootAction = _playerInput.actions["Shoot"];

		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;

		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
		{
			mouseWorldPosition = raycastHit.point;
		}

		if(_input.aim)
		{
			cm_AimVirtualCamera.gameObject.SetActive(true);
			_TPcontroller.SetSensitivity(aimSensitivity);
			_TPcontroller.SetRotateOnMove(false);
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

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
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
		}

		if(_input.shoot)
		{
			_TPcontroller.SetRotateOnMove(false);
			//animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

			if(shootAction.WasPressedThisFrame())
			{
				Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
				Instantiate(pf_BulletProjectile, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
			}

			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

			aimGameObject.transform.position = mouseWorldPosition;

			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
		}
	}

}
