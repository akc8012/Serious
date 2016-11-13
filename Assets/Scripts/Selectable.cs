using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
	[SerializeField]
	bool isHarmful = true;
	public bool IsHarmful { get { return isHarmful; } }

	Camera cam;
	bool isFlying = false;
	float flySpeed = 3;
	float distFromP = 2;
	Vector3 startPos;
	Quaternion startRot;

	void Start()
	{
		cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		startPos = transform.position;
		startRot = transform.rotation;
	}

	void Update()
	{
		
	}

	public void FlyToPlayer()
	{
		if (isFlying)
			return;

		Vector3 loc = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, distFromP));
		StartCoroutine(FlyToRoutine(loc, Quaternion.LookRotation(cam.transform.forward), true));
	}

	public void FlyToStart()
	{
		if (isFlying)
			return;

		StartCoroutine(FlyToRoutine(startPos, startRot, false));
	}

	IEnumerator FlyToRoutine(Vector3 pos, Quaternion rot, bool setMenu)
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

		if (setMenu)
			GameObject.FindWithTag("MainCanvas").transform.Find("LookAt Menu").GetComponent<LookAtUI>().SetIsVisible(true, this);
	}
}