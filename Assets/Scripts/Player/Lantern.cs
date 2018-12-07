using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace David
{
	public class Lantern : MonoBehaviour
	{
		[SerializeField] Light lanternLight;
		[SerializeField] List<Image> energyPoints = new List<Image>();
		[SerializeField] GameObject ghost;

		[SerializeField] float speed;

		Image fillBar;

		bool isActive;
		int numberEnergyPoints;
		int maxNumberOfEnergyPoints;


		private void Start()
		{
			maxNumberOfEnergyPoints = energyPoints.Count;
			numberEnergyPoints = maxNumberOfEnergyPoints;
			fillBar = energyPoints[numberEnergyPoints - 1];
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				LanternState();
			}

			if (isActive)
			{
				if (fillBar.fillAmount == 0)
				{
					numberEnergyPoints--;

					if (numberEnergyPoints == 0)
					{
						LanternState();
					}
					else
					{
						fillBar = energyPoints[numberEnergyPoints - 1];
					}
				}
				fillBar.fillAmount -= Time.deltaTime * speed;
			}

			if (!isActive)
			{
				if (fillBar.fillAmount == 1 && numberEnergyPoints == maxNumberOfEnergyPoints) { return; }
				if (fillBar.fillAmount == 1)
				{
					numberEnergyPoints++;
					fillBar = energyPoints[numberEnergyPoints - 1];
				}
				fillBar.fillAmount += Time.deltaTime * speed;
			}
		}

		private void LanternState()
		{
			if (isActive == false)
			{
				if (fillBar.fillAmount <= 1 && numberEnergyPoints <= 1) { return; }
				float amount = fillBar.fillAmount - 1f;
				fillBar.fillAmount = 0f;
				numberEnergyPoints--;
				fillBar = energyPoints[numberEnergyPoints - 1];
				if (amount < 0) { fillBar.fillAmount += amount; }
			}

			isActive = !isActive;
			lanternLight.enabled = isActive;
			ghost.SetActive(isActive);
		}
	}
}
