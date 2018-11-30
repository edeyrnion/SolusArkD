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

		bool enableAudio;


		private void Start()
		{
			musicSource.clip = musicClips[0];
			sfxSource.clip = sfxClips[0];
			musicSource.Play();
			enableAudio = true;		
		}

		public void PlayClickSound()
		{
			if (enableAudio)
			{
				sfxSource.PlayOneShot(sfxSource.clip);
			}
		}
	}
}
