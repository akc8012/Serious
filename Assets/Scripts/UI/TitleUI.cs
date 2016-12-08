using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleUI : MonoBehaviour
{
	[SerializeField]
	Button startButt;

	void Start()
	{
		startButt.onClick.AddListener(StartClicked);
	}

	void StartClicked()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}