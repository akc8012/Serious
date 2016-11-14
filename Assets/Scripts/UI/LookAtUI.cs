using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAtUI : MonoBehaviour
{
	[SerializeField]
	Button harmfulButt;
	[SerializeField]
	Button cancelButt;

	Selectable lookingAt;
	bool isVisible = false;

	void Start()
	{
		harmfulButt.onClick.AddListener(HarmfulClicked);
		cancelButt.onClick.AddListener(CancelClicked);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			CancelClicked();
	}

	void HarmfulClicked()
	{
		if (!isVisible)
			return;

		if (lookingAt.IsHarmful)
		{
			Destroy(lookingAt.gameObject);
			ScoreManager.instance.AddScore(10);
		}
		else
		{
			ScoreManager.instance.LoseScore(10);
			lookingAt.FlyToStart();
		}

		GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().SetCanLook(true);
		GameObject.FindWithTag("Player").GetComponent<Movement>().SetCanMove(true);
		GameObject.FindWithTag("Pointer").GetComponent<Pointer>().SetVisible(true);
		SetIsVisible(false);
	}

	void CancelClicked()
	{
		if (!isVisible)
			return;

		lookingAt.FlyToStart();
		GameObject.FindWithTag("MainCamera").GetComponent<MouseLook>().SetCanLook(true);
		GameObject.FindWithTag("Player").GetComponent<Movement>().SetCanMove(true);
		GameObject.FindWithTag("Pointer").GetComponent<Pointer>().SetVisible(true);
		SetIsVisible(false);
	}

	public void SetIsVisible(bool enable, Selectable _lookingAt = null)
	{
		isVisible = enable;
		gameObject.SetActive(enable);

		if (_lookingAt)
			lookingAt = _lookingAt;
	}
}