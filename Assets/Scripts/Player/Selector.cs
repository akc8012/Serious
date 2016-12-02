using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	[SerializeField]
	float maxDistance = 4;

	Camera cam;
	Pointer pointer;
	GameObject lastObjRef;
	int pSize;

	void Start()
	{
		cam = GetComponent<Camera>();
		pointer = GameObject.FindWithTag("Pointer").GetComponent<Pointer>();
		pSize = Pointer.size/2;     // must be in start to recieve after Pointer's Awake
	}

	void Update()
	{
		RaycastHit hit;
		Ray[] rays = new Ray[4]
		{
			cam.ScreenPointToRay(new Vector3(Screen.width/2 - pSize, Screen.height/2 - pSize)),
			cam.ScreenPointToRay(new Vector3(Screen.width/2 + pSize, Screen.height/2 - pSize)),
			cam.ScreenPointToRay(new Vector3(Screen.width/2 - pSize, Screen.height/2 + pSize)),
			cam.ScreenPointToRay(new Vector3(Screen.width/2 + pSize, Screen.height/2 + pSize))
		};

		bool setHover = false;

		for (int i = 0; i < 4; i++)
		{
			if (Physics.Raycast(rays[i], out hit))
			{
				Transform objectHit = hit.transform;

				if (objectHit.tag == "Selectable" && Vector3.Distance(transform.position, objectHit.position) < maxDistance)
				{
					setHover = true;

					if (lastObjRef != null && lastObjRef != objectHit.gameObject)
					{
						lastObjRef.GetComponent<Selectable>().OffHover();
						objectHit.gameObject.GetComponent<Selectable>().OnHover();
					}

					lastObjRef = objectHit.gameObject;

					if (Input.GetMouseButtonDown(0) && 
					GameStateManager.instance.CurrentState == GameStateManager.State.Free)
						SelectObject(lastObjRef);

					break;
				}
			}
		}

		if (setHover)
		{
			if (!pointer.IsHovering)
			{
				pointer.OnHover();
				if (lastObjRef != null) lastObjRef.GetComponent<Selectable>().OnHover();
			}
		}
		else if (pointer.IsHovering)
		{
			pointer.OffHover();
			if (lastObjRef != null)
			{
				lastObjRef.GetComponent<Selectable>().OffHover();
				lastObjRef = null;
			}
		}
	}

	void SelectObject(GameObject obj)
	{
		if (!obj.gameObject.GetComponent<Selectable>().IsFlying)
		{
			obj.gameObject.GetComponent<Selectable>().FlyToPlayer();
			GameStateManager.instance.SetState(GameStateManager.State.LookAt);
		}
	}
}