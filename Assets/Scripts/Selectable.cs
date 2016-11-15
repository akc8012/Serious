using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	[SerializeField]
	bool isHarmful = true;
	public bool IsHarmful { get { return isHarmful; } }

	bool isSelected = false;
	Vector2 firstDown = -Vector2.one;
	Vector2 spinDir = Vector2.zero;
	[SerializeField]
	float spinSpeed = 1;
	float spinMagSpeed = 1;

	Rigidbody rb;
	Camera cam;
	Renderer rend;
	bool isFlying = false;
	public bool IsFlying { get { return isFlying; } }
	float flySpeed = 3;
	float distFromP = 2;
	Vector3 startPos;
	Quaternion startRot;
	Color startCol;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		rend = GetComponent<Renderer>();
		startPos = transform.position;
		startRot = transform.rotation;
		startCol = rend.material.color;
		ResetValues();
	}

	void ResetValues()
	{
		isSelected = false;
		firstDown = -Vector2.one;
		rb.angularVelocity = Vector3.zero;
		spinDir = Vector2.zero;
		rb.isKinematic = true;
	}

	public void OnHover()
	{
		rend.material.SetColor("_Color", rend.material.GetColor("_Color") * 2);
	}

	public void OffHover()
	{
		rend.material.SetColor("_Color", startCol);
	}

	void Update()
	{
		if (isSelected)
		{
			if (Input.GetMouseButton(0))
			{
				if (firstDown == -Vector2.one)
					firstDown = Input.mousePosition;
				else
				{
					spinDir = (Vector2)Input.mousePosition - firstDown;

					if (spinDir.magnitude > 20)
					{
						spinMagSpeed = spinDir.magnitude * 0.20f;
						spinDir.Normalize();
					}
					else
						spinDir = Vector2.zero;
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				firstDown = -Vector2.one;
				spinDir = Vector2.zero;
			}
		}
	}

	void FixedUpdate()
	{
		if (spinDir != Vector2.zero)
		{
			rb.AddTorque(cam.transform.up * -spinDir.x * spinMagSpeed * spinSpeed * Time.deltaTime);
			rb.AddTorque(cam.transform.right * spinDir.y * spinMagSpeed * spinSpeed * Time.deltaTime);
		}
	}

	public void FlyToPlayer()
	{
		if (isFlying)
			return;

		OffHover();
		Vector3 loc = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, distFromP));
		StartCoroutine(FlyToRoutine(loc, Quaternion.LookRotation(cam.transform.forward), true));
	}

	public void FlyToStart()
	{
		if (isFlying)
			return;

		StartCoroutine(FlyToRoutine(startPos, startRot, false));
		ResetValues();
	}

	IEnumerator FlyToRoutine(Vector3 pos, Quaternion rot, bool setSelected)
	{
		isFlying = true;
		Transform startPoint = transform;
		float dist = Vector3.Distance(transform.position, pos);
		float duration = dist / flySpeed;
		float t = 0;

		while (Vector3.Distance(transform.position, pos) > 0.01f)
		{
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp(startPoint.position, pos, t);
			transform.rotation = Quaternion.Slerp(startPoint.rotation, rot, t);
			yield return null;
		}

		isFlying = false;

		if (setSelected)
		{
			GameObject.FindWithTag("MainCanvas").transform.Find("LookAt Menu").GetComponent<LookAtUI>().SetIsVisible(true, this);
			isSelected = true;
			rb.isKinematic = false;
		}
	}
}