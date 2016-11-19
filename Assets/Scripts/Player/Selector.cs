using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	Camera cam;
	Pointer pointer;
	GameObject lastObjRef;
	ExitGameUI exitGameUI;
	int pSize;

	void Start()
	{
		cam = GetComponent<Camera>();
		pointer = GameObject.FindWithTag("Pointer").GetComponent<Pointer>();
		pSize = Pointer.size/2;     // must be in start to recieve after Pointer's Awake
		exitGameUI = GameObject.FindWithTag("MainCanvas").transform.Find("ExitGame Menu").GetComponent<ExitGameUI>();
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

				if (objectHit.tag == "Selectable")
				{
					setHover = true;

					if (lastObjRef != null && lastObjRef != objectHit.gameObject)
					{
						lastObjRef.GetComponent<Selectable>().OffHover();
						objectHit.gameObject.GetComponent<Selectable>().OnHover();
					}

					lastObjRef = objectHit.gameObject;

					if (Input.GetMouseButtonDown(0) && !exitGameUI.IsVisible)
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
			GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().SetCanLook(false);
			GameObject.FindWithTag("Player").GetComponent<Movement>().SetCanMove(false);
			pointer.SetVisible(false);
		}
	}
}