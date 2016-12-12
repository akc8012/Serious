using UnityEngine;
using System.Collections;

public class Glowable : MonoBehaviour
{
	Renderer[] rends;
	Color[] startCols;
	bool isHovered = false;
	public bool IsHovered { get { return isHovered; } }

	void Start()
	{
		rends = GetComponentsInChildren<Renderer>();
		startCols = new Color[rends.Length];

		for (int i = 0; i < rends.Length; i++)
			startCols[i] = rends[i].material.color;
	}

	public void OnHover()
	{
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", rends[i].material.GetColor("_Color") * 2);
		isHovered = true;
	}

	public void OffHover()
	{
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", startCols[i]);
		isHovered = false;
	}
}
