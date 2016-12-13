using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionUI : MonoBehaviour
{
	Image whiteImg;
	[SerializeField]
	float fadeSpeed;
	[SerializeField]
	bool fadeOutOnStart = false;

	void Awake()
	{
		whiteImg = GetComponent<Image>();
		if (fadeOutOnStart) whiteImg.color = new Color(whiteImg.color.r, whiteImg.color.b, whiteImg.color.g, 1);
	}

	void Start()
	{
		if (fadeOutOnStart)
			FadeOut();
	}

	public void FadeIn(int scene)
	{
		StartCoroutine(FadeInRoutine(scene));
	}

	public void FadeOut()
	{
		StartCoroutine(FadeOutRoutine());
	}

	IEnumerator FadeInRoutine(int scene)
	{
		while (whiteImg.color.a < 0.9f)
		{
			Color col = whiteImg.color;
			col.a += fadeSpeed * Time.deltaTime;
			whiteImg.color = col;
			yield return null;
		}
		whiteImg.color = new Color(whiteImg.color.r, whiteImg.color.b, whiteImg.color.g, 1);
		LoadScene(scene);
	}

	IEnumerator FadeOutRoutine()
	{
		while (whiteImg.color.a > 0.1f)
		{
			Color col = whiteImg.color;
			col.a -= fadeSpeed * Time.deltaTime;
			whiteImg.color = col;
			yield return null;
		}
		whiteImg.color = new Color(whiteImg.color.r, whiteImg.color.b, whiteImg.color.g, 0);
		SoundManager.instance.PlayRandomMusic();
	}

	void LoadScene(int scene)
	{
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
}