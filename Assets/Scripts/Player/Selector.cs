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
			GameObject objectHit = hit.transform.gameObject;

			if (CanSelectObject(objectHit))
			{
				setHover = true;

				if (lastObjRef != null && lastObjRef != objectHit)
					lastObjRef.GetComponent<Glowable>().OffHover();

				if (!objectHit.GetComponent<Glowable>().IsHovered)
				{
					objectHit.GetComponent<Glowable>().OnHover();
					if (!pointer.IsHovering) pointer.OnHover();
				}

				if (objectHit.tag == "Selectable")
				{
					if (Input.GetMouseButtonDown(0) &&
					GameStateManager.instance.CurrentState == GameStateManager.State.Free)
						SelectObject(objectHit);
				}
				if (objectHit.tag == "Moveable")
				{
					if (Input.GetMouseButtonDown(0) &&
					GameStateManager.instance.CurrentState == GameStateManager.State.Free)
						SetMoveableObject(objectHit);
				}

				lastObjRef = objectHit;
			}
		}

		if (!setHover && pointer.IsHovering)
		{
			pointer.OffHover();
			if (lastObjRef != null)
			{
				lastObjRef.GetComponent<Glowable>().OffHover();
				lastObjRef = null;
			}
		}
	}

	bool CanSelectObject(GameObject objectHit)
	{
		bool hasTag = objectHit.tag == "Selectable" || objectHit.tag == "Moveable";
		if (!hasTag) return false;
		bool farAwayEnough = Vector3.Distance(transform.position, objectHit.transform.position) < maxDistance;
		if (!farAwayEnough) return false;

		bool canSelect = true;
		Selectable selectable = objectHit.GetComponent<Selectable>();
		if (selectable)
			canSelect = selectable.IsSelectable;

		if (!canSelect) return false;
		return true;
	}

	void SelectObject(GameObject obj)
	{
		if (!obj.GetComponent<Selectable>().IsFlying)
		{
			SoundManager.instance.PlaySound(SoundManager.instance.objClick);
			obj.GetComponent<Selectable>().FlyToPlayer();
			GameStateManager.instance.SetState(GameStateManager.State.LookAt);
		}
	}

	void SetMoveableObject(GameObject obj)
	{
		if (!obj.GetComponent<Moveable>().IsMoving)
		{
			SoundManager.instance.PlaySound(SoundManager.instance.UIclick);
			obj.GetComponent<Moveable>().MoveToNext();
		}
	}
}