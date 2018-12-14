using UnityEngine;
using UnityEngine.UI;

namespace David
{
	public class AudioManager : GameEventListener
	{
		[SerializeField] AudioClip[] musicClips;
		[SerializeField] AudioClip[] sfxClips;
		[SerializeField] AudioClip[] ambienceClips;
		[SerializeField] AudioClip[] dialogueClips;
		[SerializeField] AudioSource musicSource;
		[SerializeField] AudioSource sfxSource;
		[SerializeField] Slider musicVolumeSlider;
		[SerializeField] float volumeChangeSpeed;

		bool changeMusic;
		bool startNext;
		float volume;
		AudioClip nextClip;


		private void Start()
		{
			musicSource.clip = musicClips[0];
			musicSource.Play();
		}

		public void PlayClickSound()
		{
			sfxSource.clip = sfxClips[0];
			sfxSource.PlayOneShot(sfxSource.clip);
		}

		public void PlaySwordHitSound()
		{
			sfxSource.clip = sfxClips[1];
			sfxSource.PlayOneShot(sfxSource.clip);
		}

		public void PlayDeathSound()
		{
			sfxSource.clip = sfxClips[2];
			sfxSource.PlayOneShot(sfxSource.clip);
		}

		public void PlayBossMusic()
		{
			changeMusic = true;
			nextClip = musicClips[1];
			volume = musicSource.volume;
		}

		public void PlayGameMusic()
		{
			changeMusic = true;
			nextClip = musicClips[2];
			volume = musicSource.volume;
		}

		public void PlayBandidMusic()
		{
			changeMusic = true;
			nextClip = musicClips[3];
			volume = musicSource.volume;
		}

		public void PlayMenuMusic()
		{
			changeMusic = true;
			nextClip = musicClips[0];
			volume = musicSource.volume;
		}

		private void Update()
		{
			if (changeMusic && !startNext)
			{
				musicSource.volume -= Time.deltaTime * volumeChangeSpeed;
				if (musicSource.volume <= 0f)
				{
					musicSource.clip = nextClip;
					musicSource.Play();
					startNext = true;
				}
			}

			if (startNext)
			{
				musicSource.volume += Time.deltaTime * volumeChangeSpeed;
				if (musicSource.volume >= 1f)
				{
					changeMusic = false;
					startNext = false;
				}
			}
		}
	}
}
