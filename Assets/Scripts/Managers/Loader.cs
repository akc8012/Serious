using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
	[SerializeField]
	GameObject scoreManager;          //ScoreManager prefab to instantiate.
	[SerializeField]
	GameObject gameStateManager;

	void Awake()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (ScoreManager.instance == null)
			Instantiate(scoreManager);

		if (GameStateManager.instance == null)
			Instantiate(gameStateManager);
	}
}