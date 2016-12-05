using UnityEngine;
using System.Collections;

public class PlayerCrouch : MonoBehaviour
{
	[SerializeField]
	float crouchSpeed = 3;
	[SerializeField]
	float uncrouchSpeed = 3;

	float origY;
	float crouchY;
	bool uncrounching = false;

	void Start()
	{
		origY = transform.position.y;
		crouchY = origY - 0.7f;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.C))
		{
			if (uncrounching)
			{
				StopCoroutine("UnCrouch");
				uncrounching = false;
			}
			Crouch();
		}
		if (Input.GetKeyUp(KeyCode.C))
		{
			StartCoroutine("UnCrouch");
		}
	}

	void Crouch()
	{
		transform.position = Vector3.Lerp(transform.position,
			new Vector3(transform.position.x, crouchY, transform.position.z), Time.deltaTime * crouchSpeed);
	}

	IEnumerator UnCrouch()
	{
		uncrounching = true;
		while (Mathf.Abs(transform.position.y - origY) > 0.01f)
		{
			transform.position = Vector3.Lerp(transform.position,
				new Vector3(transform.position.x, origY, transform.position.z), Time.deltaTime * uncrouchSpeed);

			yield return null;
		}
		transform.position = new Vector3(transform.position.x, origY, transform.position.z);
		uncrounching = false;
	}
}
