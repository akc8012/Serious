using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAtUI : MonoBehaviour
{
	[SerializeField]
	Button harmfulButt;
	[SerializeField]
	Button cancelButt;
	[SerializeField]
	Text pointsText;

	CanvasGroup canvasGroup;
	Selectable lookingAt;
	bool isVisible = false;

	Color green = Color.green;
	Color red = new Color(1, 0.3f, 0.3f, 1);

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		harmfulButt.onClick.AddListener(HarmfulClicked);
		cancelButt.onClick.AddListener(CancelClicked);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			CancelClicked();
	}

	void HarmfulClicked()
	{
		if (!isVisible)
			return;

		if (lookingAt.IsHarmful)
		{
			Destroy(lookingAt.gameObject);
			ScoreManager.instance.ChangeScore(10);
			pointsText.color = green;
			pointsText.text = "+10 points";
			StartCoroutine(ShowPointsTextForABit());
		}
		else
		{
			ScoreManager.instance.ChangeScore(-10);
			lookingAt.FlyToStart();
			pointsText.color = red;
			pointsText.text = "-10 points";
			StartCoroutine(ShowPointsTextForABit());
		}

		LeaveLookAtUI();
	}

	void CancelClicked()
	{
		if (!isVisible)
			return;

		lookingAt.FlyToStart();
		LeaveLookAtUI();
	}

	void LeaveLookAtUI()
	{
		GameStateManager.instance.SetState(GameStateManager.State.Free);
		SetIsVisible(false);
	}

	public void SetIsVisible(bool enable, Selectable _lookingAt = null)
	{
		isVisible = enable;

		if (_lookingAt)
			lookingAt = _lookingAt;

		canvasGroup.alpha = enable ? 1 : 0;
	}

	IEnumerator ShowPointsTextForABit()
	{
		pointsText.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		pointsText.gameObject.SetActive(false);
	}
}