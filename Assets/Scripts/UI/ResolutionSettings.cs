using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace David
{
	public class ResolutionSettings : MonoBehaviour
	{
		[SerializeField] TMP_Dropdown resolutionDropDown;			

		Resolution[] resolutions;

		public int resWidth;
	    public int resHeight;
		public bool fullScreen;

	
		private void Awake()
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
		
		public void SetFullscreen(bool isFullScreen)
		{
			fullScreen = isFullScreen;			
		}

		public void SetResolution(int index)
		{			
			Resolution resolution = resolutions[index];
			resWidth = resolution.width;
			resHeight = resolution.height;			
		}		
	}
}
