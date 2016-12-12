using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	[SerializeField]
	float maxDistance = 4;

	Camera cam;
	Pointer pointer;
	GameObject lastObjRef;

	void Start()
	{
		cam = GetComponent<Camera>();
		pointer = GameObject.FindWithTag("Pointer").GetComponent<Pointer>();
	}

	void Update()
	{
		RaycastHit hit;

		Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

		bool setHover = false;

		if (Physics.Raycast(ray, out hit))
		{
			Transform objectHit = hit.transform;

			if ((objectHit.tag == "Selectable" || objectHit.tag == "Moveable") && 
				Vector3.Distance(transform.position, objectHit.position) < maxDistance)
			{
				setHover = true;

				if (lastObjRef != null && lastObjRef != objectHit.gameObject)
				{
					lastObjRef.GetComponent<Glowable>().OffHover();
					objectHit.gameObject.GetComponent<Glowable>().OnHover();
				}

				lastObjRef = objectHit.gameObject;

				if (objectHit.tag == "Selectable")
				{
					if (Input.GetMouseButtonDown(0) &&
					GameStateManager.instance.CurrentState == GameStateManager.State.Free)
						SelectObject(lastObjRef);
				}
				if (objectHit.tag == "Moveable")
				{
					if (Input.GetMouseButtonDown(0) &&
					GameStateManager.instance.CurrentState == GameStateManager.State.Free)
						SetMoveableObject(lastObjRef);
				}
			}
		}

		if (setHover)
		{
			if (!pointer.IsHovering)
			{
				pointer.OnHover();
				if (lastObjRef != null)
					lastObjRef.GetComponent<Glowable>().OnHover();
			}
		}
		else if (pointer.IsHovering)
		{
			pointer.OffHover();
			if (lastObjRef != null)
			{
				lastObjRef.GetComponent<Glowable>().OffHover();
				lastObjRef = null;
			}
		}
	}

	void SelectObject(GameObject obj)
	{
		if (!obj.GetComponent<Selectable>().IsFlying)
		{
			obj.GetComponent<Selectable>().FlyToPlayer();
			GameStateManager.instance.SetState(GameStateManager.State.LookAt);
		}
	}

	void SetMoveableObject(GameObject obj)
	{
		if (!obj.GetComponent<Moveable>().IsMoving)
		{
			obj.GetComponent<Moveable>().MoveToNext();
		}
	}
}