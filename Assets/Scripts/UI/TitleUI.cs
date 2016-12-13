using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleUI : MonoBehaviour
{
	[SerializeField]
	Button startButt;
	[SerializeField]
	Button quitButt;

	void Start()
	{
		startButt.onClick.AddListener(StartClicked);
		quitButt.onClick.AddListener(QuitClicked);
	}
	
	void StartClicked()
	{
		SoundManager.instance.PlaySound(SoundManager.instance.gameStartClick);
		transform.Find("White Fade").GetComponent<SceneTransitionUI>().FadeIn(1);
	}

	void QuitClicked()
	{
		SoundManager.instance.PlaySound(SoundManager.instance.UIclickNeg, 3);
		Application.Quit();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}