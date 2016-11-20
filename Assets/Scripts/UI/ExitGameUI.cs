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
		exitButton.onClick.AddListener(ExitRoom);
		stayButton.onClick.AddListener(StayInRoom);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isVisible && 
		GameStateManager.instance.CurrentState == GameStateManager.State.Free)
			SetIsVisible(true);
	}
	
	void ExitRoom()
	{
		if (!isVisible)
			return;

		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

	void StayInRoom()
	{
		if (!isVisible)
			return;

		SetIsVisible(false);
	}

	void SetIsVisible(bool enable)
	{
		isVisible = enable;
		canvasGroup.alpha = enable ? 1 : 0;
		GameStateManager.instance.SetState(enable ? GameStateManager.State.InMenu : GameStateManager.State.Free);
		exitText.SetActive(!enable);
	}
}
