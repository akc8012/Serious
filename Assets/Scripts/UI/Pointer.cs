using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pointer : MonoBehaviour
{
	public static int size;

	Image image;
	bool isHovering = false;
	public bool IsHovering { get { return isHovering; } }

	void Awake()
	{
		image = GetComponent<Image>();
		size = (int)image.rectTransform.rect.width;		// must be in Awake to send to Selector's Start
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