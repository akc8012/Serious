using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultsMenuUI : MonoBehaviour
{
	CanvasGroup canvasGroup;
	bool isVisible = false;
	Color textStartColor;

	[SerializeField]
	Text foundText;
	[SerializeField]
	Text missedText;
	[SerializeField]
	Text missedPointsText;
	[SerializeField]
	Text finalScoreText;
	[SerializeField]
	Button yesButt;
	[SerializeField]
	Button noButt;

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		yesButt.onClick.AddListener(YesClicked);
		noButt.onClick.AddListener(NoClicked);
		textStartColor = foundText.color;
	}

	void YesClicked()
	{
		// reload scene
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	void NoClicked()
	{
		// go to scene 0
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

	void FillTextBox(Text textBox, List<string> names)
	{
		textBox.text = "";
		textBox.color = textStartColor;
		missedPointsText.text = "";

		for (int i = 0; i < names.Count; i++)
		{
			textBox.text += names[i];
			textBox.text += "\n";

			if (textBox == missedText)
				missedPointsText.text += "-500 points\n";
		}
	}

	void FillTextBoxes()
	{
		FillTextBox(foundText, ScoreManager.instance.GetFoundObjects());
		FillTextBox(missedText, ScoreManager.instance.GetRemainingObjects());

		Color darkGray = new Color(0.4f, 0.4f, 0.4f, 1);
		if (foundText.text == "")
		{
			foundText.text = "You didn't find anything!";
			foundText.color = darkGray;
		}
		if (missedText.text == "")
		{
			missedText.text = "No harmful objects left!";
			missedText.color = darkGray;
		}

		finalScoreText.text = "Final score: " + ScoreManager.instance.GetAndSetFinalScore();
	}

	public void SetIsVisible(bool enable)
	{
		isVisible = enable;
		canvasGroup.alpha = enable ? 1 : 0;

		if (isVisible)
			FillTextBoxes();
	}
}