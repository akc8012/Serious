using UnityEngine;
using System.Collections;

public class VisibilityTrigger : MonoBehaviour
{
	[SerializeField]
	GameObject[] groupA;
	[SerializeField]
	GameObject[] groupB;
	
	enum AxisToCheck { X, Y, Z };
	[SerializeField]
	AxisToCheck axisToCheck = AxisToCheck.Z;

	Vector3 direction;

	void OnTriggerEnter(Collider other)
	{
		direction = other.transform.position;
	}

	void OnTriggerExit(Collider other)
	{
		direction -= other.transform.position;

		if (direction[(int)axisToCheck] >= 0)
		{
			SetVisible(groupA, false);
			SetVisible(groupB, true);
		}
		else
		{
			SetVisible(groupB, false);
			SetVisible(groupA, true);
		}
	}

	void SetVisible(GameObject[] group, bool enable)
	{
		for (int i = 0; i < group.Length; i++)
		{
			group[i].SetActive(enable);
		}
	}
}