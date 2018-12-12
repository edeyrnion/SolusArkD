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
	}
}
