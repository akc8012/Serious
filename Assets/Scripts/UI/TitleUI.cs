﻿using UnityEngine;
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
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

	void QuitClicked()
	{
		Application.Quit();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}