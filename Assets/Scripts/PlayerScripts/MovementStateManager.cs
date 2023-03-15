using UnityEngine;
using StarterAssets;

public class MovementStateManager : MonoBehaviour
{
	[SerializeField] public float moveSpeed = 3f;
	[SerializeField] public Vector3 direction;
	float horizontal_Input, vertical_Input;

	[SerializeField] float groundYOffset;
	[SerializeField] float gravity = 9.81f;
	[SerializeField] LayerMask groundLayerMask;
	Vector3 spherePos;
	Vector3 velocity;

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
		Gravity();
	}

	void GetDirectionAndMove()
	{
		Vector2 moveInput = _inputs.move;
		horizontal_Input = moveInput.x;
		vertical_Input = moveInput.y;

		direction = transform.forward * vertical_Input + transform.right * horizontal_Input;

		_characterController.Move(direction * moveSpeed * Time.deltaTime);
	}

	bool IsGrounded()
	{
		spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
		if(Physics.CheckSphere(spherePos, _characterController.radius - 0.05f,groundLayerMask)) return true;
		return false;
	}

	void Gravity()
	{
		if (!IsGrounded())
		{
			velocity.y += gravity * Time.deltaTime;
		}
		else if (velocity.y < 0)
		{
			velocity.y = -2;       // to make sure char always touching ground
		}

		_characterController.Move(velocity * Time.deltaTime);
	}

	//private void OnDrawGizmos()
	//{
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawSphere(spherePos, _characterController.radius - 0.05f);
	//}
}
