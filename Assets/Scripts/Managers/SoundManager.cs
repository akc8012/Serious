using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;

	AudioSource audioSource;

	public AudioClip objClick;
	public AudioClip UIclick;
	public AudioClip UIclickNeg;
	public AudioClip getPoints;
	public AudioClip losePoints;
	public AudioClip gameStartClick;

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
		audioSource = GetComponent<AudioSource>();
	}

	public void PlaySound(AudioClip clip, float volumeScale = 1)
	{
		audioSource.PlayOneShot(clip, volumeScale);
	}
}
