using UnityEngine;
using System.Collections;

public class Glowable : MonoBehaviour
{
	Renderer[] rends;
	Color[] startCols;
	bool isHovered = false;
	public bool IsHovered { get { return isHovered; } }
	bool isGlowable = true;
	public bool IsGlowable { get { return isGlowable; } }

	void Start()
	{
		rends = GetComponentsInChildren<Renderer>();
		startCols = new Color[rends.Length];

		for (int i = 0; i < rends.Length; i++)
			startCols[i] = rends[i].material.color;
	}

	public void OnHover()
	{
		if (!isGlowable) return;
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", rends[i].material.GetColor("_Color") * 2);
		isHovered = true;
	}

	public void OffHover()
	{
		if (!isGlowable) return;
		for (int i = 0; i < rends.Length; i++)
			rends[i].material.SetColor("_Color", startCols[i]);
		isHovered = false;
	}

	public void SetIsGlowable(bool enable)
	{
		isGlowable = enable;
		if (!isGlowable)
			OffHover();
	}
}
