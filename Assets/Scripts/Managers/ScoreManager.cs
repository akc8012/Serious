using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance = null;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{

	}

	void Update()
	{
		
	}
}