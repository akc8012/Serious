using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	int score = 0;
	Text scoreText;

	class HarmfulObject
	{
		public string name;
		public int amount;

		public HarmfulObject(string name, int amount)
		{
			this.name = name;
			this.amount = amount;
		}

		public string text { get { return name + " (" + amount + ")"; } }
	}

	List<HarmfulObject> remainingObjects = new List<HarmfulObject>();
	List<HarmfulObject> foundObjects = new List<HarmfulObject>();

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
		score = 0;
		remainingObjects.Clear();
		foundObjects.Clear();

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

	public string ChangeScore(int points)
	{
		score += points;
		if (score <= 0) score = 0;

		if (scoreText)
		{
			string formattedText = score.ToString("00000000");
			scoreText.text = "Score: " + formattedText;
			return formattedText;
		}
		else
			return "";
	}

	string GetRealName(string objName)
	{
		string realName = "";

		//Selectable
		for (int i = 11; i < objName.Length; i++)
		{
			if (objName[i] != '(')
				realName += objName[i];
			else
				break;
		}

		return realName;
	}

	public void AddHarmfulObject(string objName)
	{
		string realName = GetRealName(objName);
		AddObject(realName, remainingObjects);
	}

	void AddObject(string realName, List<HarmfulObject> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (realName == list[i].name)
			{
				list[i].amount++;
				return;
			}
		}

		HarmfulObject obj = new HarmfulObject(realName, 1);
		list.Add(obj);
	}

	public void RemoveHarmfulObject(string objName)
	{
		string realName = GetRealName(objName);

		for (int i = 0; i < remainingObjects.Count; i++)
		{
			if (realName == remainingObjects[i].name)
			{
				remainingObjects[i].amount--;
				AddObject(remainingObjects[i].name, foundObjects);

				if (remainingObjects[i].amount <= 0)
					remainingObjects.RemoveAt(i);

				return;
			}
		}
	}

	List<string> GetObjects(List<HarmfulObject> list)
	{
		List<string> names = new List<string>();

		for (int i = 0; i < list.Count; i++)
			names.Add(list[i].text);

		return names;
	}

	public List<string> GetRemainingObjects()
	{
		return GetObjects(remainingObjects);
	}

	public List<string> GetFoundObjects()
	{
		return GetObjects(foundObjects);
	}
	
	public string GetAndSetFinalScore()
	{
		int deduction = remainingObjects.Count*500;
		return ChangeScore(-deduction);
	}
}