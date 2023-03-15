using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Threading;

public class AimStateManager : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private Transform followTarget;
	[SerializeField] private float horizontalSpeed = 2.0f;
	[SerializeField] private float verticalSpeed = 2.0f;
	[SerializeField] private float minVerticalAngle = -30.0f;
	[SerializeField] private float maxVerticalAngle = 30.0f;

	public Cinemachine.AxisState xAxis;

	private float mouseX;
	private float mouseY;

	private Vector2 cameraInput;

	private void OnLook(InputValue value)
	{
		cameraInput = value.Get<Vector2>();
	}

	private void LateUpdate()
	{
		if (followTarget == null)
			return;

		// Rotate the camera horizontally
		transform.RotateAround(followTarget.position, Vector3.up, cameraInput.x * horizontalSpeed);

		// Rotate the camera vertically
		var rotation = virtualCamera.transform.localRotation.eulerAngles;
		rotation.x -= cameraInput.y * verticalSpeed;
		rotation.x = Mathf.Clamp(rotation.x, minVerticalAngle, maxVerticalAngle);

		mouseX += cameraInput.x * horizontalSpeed;
		mouseY -= cameraInput.y * verticalSpeed;
		mouseY = Mathf.Clamp(mouseY, minVerticalAngle, maxVerticalAngle);

		virtualCamera.Follow = followTarget;
	}
}
