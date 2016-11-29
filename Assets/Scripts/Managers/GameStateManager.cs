using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
	public static GameStateManager instance = null;

	public enum State { None, Free, LookAt, InMenu };
	State state;
	public State CurrentState { get { return state; } }

	MouseLook mouseLook;
	Movement movement;
	Pointer pointer;
	ArrowHelperUI arrowHelperUI;
	bool hasRefs = false;

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
		if (GetReferences())
			SetState(State.InMenu);
		else
			state = State.None;
	}

	bool GetReferences()
	{
		try
		{
			mouseLook = GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>();
			movement = GameObject.FindWithTag("Player").GetComponent<Movement>();
			pointer = GameObject.FindWithTag("Pointer").GetComponent<Pointer>();
			arrowHelperUI = GameObject.FindWithTag("MainCanvas").transform.FindChild("ArrowHelpers").GetComponent<ArrowHelperUI>();
		}
		catch
		{
			return false;
		}
		
		hasRefs = (mouseLook && movement && pointer && arrowHelperUI);
		return hasRefs;
	}

	public bool SetState(State newState)
	{
		if (!hasRefs) return false;

		state = newState;
		switch (state)
		{
			case State.Free:
				mouseLook.SetCanLook(true);
				movement.SetCanMove(true);
				pointer.SetVisible(true);
				arrowHelperUI.SetVisible(true);
			break;

			case State.LookAt:
			case State.InMenu:
				mouseLook.SetCanLook(false);
				movement.SetCanMove(false);
				pointer.SetVisible(false);
				arrowHelperUI.SetVisible(false);
				break;
		}

		return true;
	}
}