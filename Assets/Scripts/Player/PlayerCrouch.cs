using UnityEngine;
using System.Collections;

public class PlayerCrouch : MonoBehaviour
{
	[SerializeField]
	float crouchDist = 0.7f;
	[SerializeField]
	float crouchSpeed = 3;
	[SerializeField]
	float uncrouchSpeed = 3;

	float origY;
	float crouchY;
	enum State { Nothing, Crouching, Uncrouching }
	State crouchState = State.Nothing;
	State lastState = State.Nothing;
	public bool IsCrouching { get { return crouchState == State.Crouching; } }
	Movement movement;

	void Start()
	{
		movement = GetComponentInParent<Movement>();
		origY = transform.position.y;
		crouchY = origY - crouchDist;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.C))
		{
			if (crouchState == State.Uncrouching)
				StopCoroutine("UnCrouch");

			crouchState = State.Crouching;
			Crouch();
		}
		else if (Input.GetKeyUp(KeyCode.C))
			StartCoroutine("UnCrouch");

		if (lastState != crouchState)
			movement.SetCrouchSpeed(IsCrouching);
		lastState = crouchState;
	}

	void Crouch()
	{
		transform.position = Vector3.Lerp(transform.position,
			new Vector3(transform.position.x, crouchY, transform.position.z), Time.deltaTime * crouchSpeed);
	}

	IEnumerator UnCrouch()
	{
		crouchState = State.Uncrouching;
		while (Mathf.Abs(transform.position.y - origY) > 0.01f)
		{
			transform.position = Vector3.Lerp(transform.position,
				new Vector3(transform.position.x, origY, transform.position.z), Time.deltaTime * uncrouchSpeed);

			yield return null;
		}
		transform.position = new Vector3(transform.position.x, origY, transform.position.z);
		crouchState = State.Nothing;
	}
}
