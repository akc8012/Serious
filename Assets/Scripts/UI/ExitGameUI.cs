using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitGameUI : MonoBehaviour
{
	CanvasGroup canvasGroup;

	[SerializeField]
	GameObject exitText;
	[SerializeField]
	Button exitButton;
	[SerializeField]
	Button stayButton;
	bool isVisible = false;
	public bool IsVisible { get { return isVisible; } }

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		exitButton.onClick.AddListener(EndGame);
		stayButton.onClick.AddListener(StayInRoom);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isVisible &&
		GameStateManager.instance.CurrentState == GameStateManager.State.Free)
		{
			SoundManager.instance.PlaySound(SoundManager.instance.UIclick);
			SetIsVisible(true, true);
		}
	}
	
	void EndGame()
	{
		if (!isVisible) return;

		SoundManager.instance.PlaySound(SoundManager.instance.UIclick);
		transform.parent.Find("Results Menu").GetComponent<ResultsMenuUI>().SetIsVisible(true);
		SetIsVisible(false, false);
	}

	void StayInRoom()
	{
		if (!isVisible) return;

		SoundManager.instance.PlaySound(SoundManager.instance.UIclickNeg, 3);
		SetIsVisible(false, true);
	}

	void SetIsVisible(bool enable, bool changeState)
	{
		isVisible = enable;
		canvasGroup.alpha = enable ? 1 : 0;

		if (changeState)
		{
			GameStateManager.instance.SetState(enable ? GameStateManager.State.InMenu : GameStateManager.State.Free);
			exitText.SetActive(!enable);
		}
	}
}
