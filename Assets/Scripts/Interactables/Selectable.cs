using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	[SerializeField]
	bool isHarmful = true;
	public bool IsHarmful { get { return isHarmful; } }
	[SerializeField]
	float flySpeed = 3;
	[SerializeField]
	float distFromCamera = 1;
	[SerializeField]
	Vector3 rotationStart;

	bool isSelected = false;
	Vector2 firstDown = -Vector2.one;
	Vector2 spinDir = Vector2.zero;
	[SerializeField]
	float spinSpeed = 1;
	float spinMagSpeed = 1;

	Rigidbody rb;
	Camera cam;
	Glowable glowable;
	bool isFlying = false;
	public bool IsFlying { get { return isFlying; } }
	Vector3 startPos;
	Quaternion startRot;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		glowable = GetComponent<Glowable>();

		startPos = transform.position;
		startRot = transform.rotation;
		ResetValues();
	}

	public void SetFloorPoint(Vector3 target)
	{
		Transform spawnFloor = transform.Find("SpawnFloor");
		if (spawnFloor)
		{
			Vector3 distance = target - spawnFloor.position;
			transform.position += distance;
		}
	}

	void ResetValues()
	{
		isSelected = false;
		firstDown = -Vector2.one;
		rb.angularVelocity = Vector3.zero;
		spinDir = Vector2.zero;
		rb.isKinematic = true;
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
			rb.AddTorque(cam.transform.up * -spinDir.x * spinMagSpeed * spinSpeed * Time.deltaTime, ForceMode.Impulse);
			rb.AddTorque(cam.transform.right * spinDir.y * spinMagSpeed * spinSpeed * Time.deltaTime, ForceMode.Impulse);
		}
	}

	public void FlyToPlayer()
	{
		if (isFlying)
			return;

		glowable.OffHover();
		Vector3 loc = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, distFromCamera));
		StartCoroutine(FlyToRoutine(loc, Quaternion.LookRotation(cam.transform.forward) * Quaternion.Euler(rotationStart), true));
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