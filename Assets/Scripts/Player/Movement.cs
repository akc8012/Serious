using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	float speed = 0;

	[SerializeField]
	float maxSpeed = 8;
	[SerializeField]
	float acceleration = 0.3f;
	[SerializeField]
	float deceleration = 0.6f;


	CharacterController controller;
	Vector2 lastInput;
	Vector3 lastForward;
	Vector3 lastRight;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
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

		if (speed - controller.velocity.magnitude > (maxSpeed*0.25f))
			SlowDown();

		velocity.Normalize();
		velocity *= speed;

		controller.Move(velocity * Time.deltaTime);
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
}