using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	float speed = 0;

	[SerializeField]
	float maxWalkSpeed = 4;
	[SerializeField]
	float maxCrouchSpeed = 1.5f;
	[SerializeField]
	float acceleration = 0.3f;
	[SerializeField]
	float deceleration = 0.6f;

	CharacterController controller;
	Vector2 lastInput;
	Vector3 lastForward;
	Vector3 lastRight;
	float maxSpeed;
	float startY;

	bool canMove = true;
	public bool CanMove { get { return canMove; } }

	void Start()
	{
		controller = GetComponent<CharacterController>();
		maxSpeed = maxWalkSpeed;
		startY = transform.position.y;
	}

	void Update()
	{
		if (!canMove)
			return;

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		bool isMoving = (input != Vector2.zero);
		if (isMoving)
		{
			lastInput = input;
			lastForward = transform.forward;
			lastRight = transform.right;

			SpeedUp();
		}
		else
			SlowDown();

		Vector3 velocity = isMoving ? input.y * transform.forward : lastInput.y * lastForward;
		velocity += isMoving ? input.x * transform.right : lastInput.x * lastRight;

		if (speed - controller.velocity.magnitude > (maxSpeed * 0.25f))
			SlowDown();

		velocity.Normalize();
		velocity *= speed;

		controller.Move(velocity * Time.deltaTime);

		Vector3 pos = transform.position;
		if (pos.y != startY)
		{
			pos.y = startY;
			transform.position = pos;
		}
	}

	void SpeedUp()
	{
		speed += acceleration;
		speed = Mathf.Clamp(speed, 0, maxSpeed);
	}

	void SlowDown()
	{
		speed -= deceleration;
		speed = Mathf.Clamp(speed, 0, maxSpeed);
	}

	public void SetCrouchSpeed(bool isCrouching)
	{
		if (isCrouching)
			maxSpeed = maxCrouchSpeed;
		else
			maxSpeed = maxWalkSpeed;
	}

	public void SetCanMove(bool enable)
	{
		canMove = enable;

		if (!canMove)
			speed = 0;
	}
}