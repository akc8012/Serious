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
			SetIsVisible(true, true);
	}
	
	void EndGame()
	{
		if (!isVisible)
			return;

		//SceneManager.LoadScene(0, LoadSceneMode.Single);

		transform.parent.Find("Results Menu").GetComponent<ResultsMenuUI>().SetIsVisible(true);
		SetIsVisible(false, false);
	}

	void StayInRoom()
	{
		if (!isVisible)
			return;

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
