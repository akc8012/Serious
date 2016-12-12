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
		if (Input.GetKeyUp(KeyCode.Escape))
			CancelClicked();
	}

	void HarmfulClicked()
	{
		if (!isVisible)
			return;

		if (lookingAt.IsHarmful)
		{
			ScoreManager.instance.RemoveHarmfulObject(lookingAt.gameObject.name);
			//Destroy(lookingAt.gameObject);
			lookingAt.ShrinkThenDie();
			ObjectClicked(1000, green);
		}
		else
		{
			lookingAt.FlyToStart();
			ObjectClicked(-250, red);
		}

		LeaveLookAtUI();
	}

	void ObjectClicked(int scoreChange, Color pointsColor)
	{
		ScoreManager.instance.ChangeScore(scoreChange);
		pointsText.color = pointsColor;
		pointsText.text = (scoreChange >= 0 ? "+" : "") + scoreChange + " points";
		StartCoroutine(ShowPointsTextForABit());
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
