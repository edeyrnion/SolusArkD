using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace David
{
	public class ResolutionSettings : MonoBehaviour
	{
		[SerializeField] TMP_Dropdown resolutionDropDown;
		Resolution[] resolutions;


		private void Start()
		{
			resolutions = Screen.resolutions;
			resolutionDropDown.ClearOptions();
			List<string> options = new List<string>();

			int currentResolutionIndex = 0;
			for (int i = 0; i < resolutions.Length; i++)
			{
				string option = $"{resolutions[i].width} x {resolutions[i].height}";
				options.Add(option);

				if (resolutions[i].width == Screen.currentResolution.width &&
					resolutions[i].height == Screen.currentResolution.height)
				{
					currentResolutionIndex = i;
				}
			}

			resolutionDropDown.AddOptions(options);
			resolutionDropDown.value = currentResolutionIndex;
			resolutionDropDown.RefreshShownValue();
		}

		public void SetFullscrren(bool isFullScreen)
		{
			Screen.fullScreen = isFullScreen;
		}

		public void SetResolution(int resolutionIndex)
		{
			Resolution resolution = resolutions[resolutionIndex];
			Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		}
	}
}
