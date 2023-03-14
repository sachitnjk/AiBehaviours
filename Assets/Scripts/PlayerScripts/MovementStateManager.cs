using UnityEngine;
using StarterAssets;

public class MovementStateManager : MonoBehaviour
{
	public float moveSpeed = 3f;
	public Vector3 direction;
	float horizontal_Input, vertical_Input;

	CharacterController _characterController;
	StarterAssetsInputs _inputs;

	

	private void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_inputs = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
		GetDirectionAndMove();
	}

	void GetDirectionAndMove()
	{
		Vector2 moveInput = _inputs.move;
		horizontal_Input = moveInput.x;
		vertical_Input = moveInput.y;

		direction = transform.forward * vertical_Input + transform.right * horizontal_Input;

		_characterController.Move(direction * moveSpeed * Time.deltaTime);
	}
}
