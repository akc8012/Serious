using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pointer : MonoBehaviour
{
	Image image;
	bool isHovering = false;
	public bool IsHovering { get { return isHovering; } }

	void Awake()
	{
		image = GetComponent<Image>();
	}

	void Update()
	{
		
	}

	public void OnHover()
	{
		image.color = Color.yellow;
		isHovering = true;
	}

	public void OffHover()
	{
		image.color = Color.white;
		isHovering = false;
	}

	public void SetVisible(bool enable)
	{
		Color col = image.color;
		col.a = enable ? 1 : 0;
		image.color = col;
	}
}