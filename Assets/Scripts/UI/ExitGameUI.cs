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

	MouseLook mouseLook;

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		exitButton.onClick.AddListener(ExitRoom);
		stayButton.onClick.AddListener(StayInRoom);
		mouseLook = GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>();

		SetIsVisible(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isVisible && mouseLook.CanLook)
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
		mouseLook.SetCanLook(!enable);
		GameObject.FindWithTag("Player").GetComponent<Movement>().SetCanMove(!enable);
		GameObject.FindWithTag("Pointer").GetComponent<Pointer>().SetVisible(!enable);
		exitText.SetActive(!enable);
	}
}
