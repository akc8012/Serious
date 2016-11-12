using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pointer : MonoBehaviour
{
	public static int size;

	Image image;
	bool isHovering = false;
	public bool IsHovering { get { return isHovering; } }

	void Start()
	{
		image = GetComponent<Image>();
		size = (int)image.rectTransform.rect.width;
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
}