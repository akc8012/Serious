using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowHelperUI : MonoBehaviour
{
	MouseLook mouseLook;

	[SerializeField]
	Image greenArrow;
	[SerializeField]
	Image redArrow;
	[SerializeField]
	Text greenText;
	[SerializeField]
	Text redText;

	float turningCounter;
	bool doCount = true;

	void Start()
	{
		mouseLook = GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>();
		SetAlpha(0);
	}

	void Update()
	{
		float delta = mouseLook.DeltaRotation;

		if (doCount)
		{
			turningCounter += delta;
			//print(turningCounter);
		}

		if (turningCounter >= 60 || turningCounter <= -60)
		{
			StartCoroutine(TextOnOff(Mathf.Sign(turningCounter)));
			//print(Mathf.Sign(turningCounter));
			turningCounter = 0;
			doCount = false;
		}
	}

	IEnumerator TextOnOff(float whichText)
	{
		Text changeText = whichText > 0 ? greenText : redText;

		changeText.color = new Color(changeText.color.r, changeText.color.g, changeText.color.b, 1);
		yield return new WaitForSeconds(0.5f);
		changeText.color = new Color(changeText.color.r, changeText.color.g, changeText.color.b, 0);
		doCount = true;
	}

	void SetAlpha(float alpha)
	{
		greenArrow.color = new Color(greenArrow.color.r, greenArrow.color.g, greenArrow.color.b, alpha);
		redArrow.color = new Color(redArrow.color.r, redArrow.color.g, redArrow.color.b, alpha);
		greenText.color = new Color(greenText.color.r, greenText.color.g, greenText.color.b, alpha);
		redText.color = new Color(redText.color.r, redText.color.g, redText.color.b, alpha);
	}
}