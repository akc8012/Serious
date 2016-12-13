using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;

	[SerializeField]
	AudioSource clipSource;
	[SerializeField]
	AudioSource musicSource;

	public AudioClip objClick;
	public AudioClip UIclick;
	public AudioClip UIclickNeg;
	public AudioClip getPoints;
	public AudioClip losePoints;
	public AudioClip gameStartClick;
	public AudioClip pop;

	public AudioClip music0;
	public AudioClip music1;
	public AudioClip music2;
	AudioClip[] musics;

	enum SoundState { AllPlaying, OnlySfx, Nothing, Count }
	SoundState soundState = SoundState.AllPlaying;

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
		musics = new AudioClip[] { music0, music1, music2 };
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			soundState++;
			if (soundState >= SoundState.Count)
				soundState = 0;
			
			if (soundState == SoundState.AllPlaying)
				musicSource.UnPause();
			else
				musicSource.Pause();
		}
	}

	public void PlaySound(AudioClip clip, float volumeScale = 1)
	{
		if (soundState != SoundState.Nothing)
			clipSource.PlayOneShot(clip, volumeScale);
	}

	public void PlayRandomMusic()
	{
		if (soundState == SoundState.AllPlaying)
		{
			musicSource.clip = musics[Random.Range(0, musics.Length)];
			musicSource.Play();
		}
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}
}
