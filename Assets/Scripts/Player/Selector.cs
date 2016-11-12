using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour
{
	Camera cam;
	Pointer pointer;
	int pSize;

	void Start()
	{
		cam = GetComponent<Camera>();
		pointer = GameObject.FindWithTag("Pointer").GetComponent<Pointer>();
		pSize = Pointer.size/2;
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
					break;
				}
			}
		}

		if (setHover)
		{
			if (!pointer.IsHovering)
				pointer.OnHover();
		}
		else if (pointer.IsHovering)
		{
			pointer.OffHover();
		}
	}
}