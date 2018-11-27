using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionChanger : MonoBehaviour
{
	[SerializeField] TMP_Dropdown resolutionDropDown;
	List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
	Resolution[] resolutions;
	bool fullscreen;


	void Start()
	{
		resolutionDropDown.ClearOptions();
		resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			int width = resolutions[i].width;
			int height = resolutions[i].height;
			TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
			optionData.text = $"{width}x{height}";
			optionDatas.Add(optionData);
		}
		resolutionDropDown.AddOptions(optionDatas);
		int currentWidth = Screen.currentResolution.width;
		int currentHeight = Screen.currentResolution.height;
		string currentRes = $"{currentWidth}x{currentHeight}";
		for (int i = 0; i < resolutionDropDown.options.Count; i++)
		{
			if (resolutionDropDown.options[i].text == currentRes)
			{
				int value = i;
				resolutionDropDown.value = value;
				return;
			}
		}
	}

	void Update()
	{

	}
}
