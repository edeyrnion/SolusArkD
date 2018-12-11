using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace David
{
	public class Lantern : MonoBehaviour
	{
		[SerializeField] BossManager bossManager;
		[SerializeField] Light lanternLight;
		[SerializeField] TextMeshProUGUI lanternUsesText;
		[SerializeField] Image fillBar;
		[SerializeField] ParticleSystem particles;
		[SerializeField] GameObject ghost;

		[SerializeField] float fillSpeed;

		public int MaxLanternUses;
		public bool isActive;

		int lanternUses;

		private void Start()
		{
			fillBar.fillAmount = 0.78f;
			lanternUses = MaxLanternUses;
			UpdateText();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (!isActive)
				{
					if (lanternUses <= 0) { return; }
					fillBar.fillAmount = 0.78f;
					lanternUses--;
					UpdateText();
				}

				LanternState();
			}

			if (isActive)
			{
				if (particles.isPlaying == true) { particles.Stop(); }
				fillBar.fillAmount -= Time.deltaTime * fillSpeed;
				if (fillBar.fillAmount <= 0.185f)
				{
					LanternState();
				}
			}

			if (!isActive)
			{
				if (fillBar.fillAmount >= 0.78f && lanternUses == MaxLanternUses) { return; }
				if (particles.isPlaying == false) { particles.Play(); }

				fillBar.fillAmount += Time.deltaTime * fillSpeed;
				if (fillBar.fillAmount >= 0.78f)
				{
					lanternUses++;
					UpdateText();
					if (lanternUses == MaxLanternUses)
					{
						if (particles.isPlaying == true) { particles.Stop(); }
						return;
					}
					fillBar.fillAmount = 0.185f;
				}
			}
		}

		private void LanternState()
		{
			isActive = !isActive;
			lanternLight.enabled = isActive;
			if (bossManager.state == BossState.Prone) { ghost.SetActive(isActive); }
		}

		private void UpdateText()
		{
			lanternUsesText.text = $"x{lanternUses}";
		}
	}
}
