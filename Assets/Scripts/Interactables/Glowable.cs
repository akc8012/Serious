using UnityEngine;
using System.Collections;

public class Glowable : MonoBehaviour
{
	Renderer[] rends;
	Color startCol;

	void Start()
	{
		rends = GetComponentsInChildren<Renderer>();
		startCol = rends[0].material.color;
	}

	public void OnHover()
	{
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", rends[i].material.GetColor("_Color") * 2);
	}

	public void OffHover()
	{
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", startCol);
	}
}
