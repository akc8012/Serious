using UnityEngine;
using System.Collections;

public class PatientInfoUI : MonoBehaviour
{
	bool isVisible = true;

	void Start()
	{
		
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && isVisible)
			SetIsVisible(false);
	}

	public void SetIsVisible(bool enable)
	{
		isVisible = enable;
		GameStateManager.instance.SetState(enable ? GameStateManager.State.InMenu : GameStateManager.State.Free);
		gameObject.SetActive(enable);
	}
}