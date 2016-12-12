using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowHelperUI : MonoBehaviour
{
	MouseLook mouseLook;
	CanvasGroup canvasGroup;

	[SerializeField]
	Image greenArrow;
	[SerializeField]
	Image greenArrowGlow;
	[SerializeField]
	Image redArrow;
	[SerializeField]
	Image redArrowGlow;
	[SerializeField]
	Text greenText;
	[SerializeField]
	Text redText;

	MaskableGraphic[] allGraphics;

	float turningCounter;
	bool doCount = true;
	const int DIST = 80;

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		mouseLook = GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>();
		allGraphics = new MaskableGraphic[] { greenArrow, greenArrowGlow, redArrow, redArrowGlow, greenText, redText };
		for (int i = 0; i < allGraphics.Length; i++)
			SetAlpha(allGraphics[i], 0);
	}

	void Update()
	{
		if (doCount)
			turningCounter += mouseLook.DeltaRotation;

		SetAlpha(greenArrow, turningCounter/DIST > 0.3f ? turningCounter/DIST : 0.3f);
		SetAlpha(redArrow, ((turningCounter/-DIST)*0.7f)+0.3f);

		if ((turningCounter >= DIST || turningCounter <= -DIST) && doCount)
		{
			StartCoroutine(ArrowFinish((int)Mathf.Sign(turningCounter)));
			doCount = false;
		}
	}

	IEnumerator ArrowFinish(int whichText)
	{
		ScoreManager.instance.ChangeScore(whichText >= 0 ? 100 : -50);
		Text changeText = whichText > 0 ? greenText : redText;
		StartCoroutine(GlowArrow(whichText > 0 ? greenArrowGlow : redArrowGlow));

		SetAlpha(changeText, 1);
		yield return new WaitForSeconds(0.5f);
		SetAlpha(changeText, 0);
		doCount = true;
	}

	IEnumerator GlowArrow(Image arrow)
	{
		float i = 0;
		while (arrow.color.a < 1)
		{
			i += 0.15f;
			SetAlpha(arrow, i);
			yield return null;
		}
		i = 1;
		turningCounter = 0;
		//yield return new WaitForSeconds(0.25f);

		while (arrow.color.a > 0)
		{
			i -= 0.085f;
			SetAlpha(arrow, i);
			yield return null;
		}
		SetAlpha(arrow, 0);
	}

	void SetAlpha(MaskableGraphic obj, float alpha)
	{
		obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, alpha);
	}

	public void SetVisible(bool enable)
	{
		doCount = enable;
		// put turningCounter = 0 here if you want to reset the arrows after selecting an object
		canvasGroup.alpha = enable ? 1 : 0;
	}
}