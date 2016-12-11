using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	int score = 0;
	Text scoreText;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += (scene, loadingMode) => { SceneLoaded(); };
	}

	void SceneLoaded()
	{
		if (GameObject.FindWithTag("MainCanvas"))
		{
			if (GameObject.FindWithTag("MainCanvas").transform.Find("Score Text"))
			{
				scoreText = GameObject.FindWithTag("MainCanvas").transform.Find("Score Text").GetComponent<Text>();

				if (scoreText)
					scoreText.text = "Score: " + score.ToString("00000000");
			}
		}
	}

	void Update()
	{
		
	}

	public void ChangeScore(int points)
	{
		score += points;
		if (score <= 0) score = 0;

		if (scoreText)
			scoreText.text = "Score: " + score.ToString("00000000");
	}
}