using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionChanger : MonoBehaviour
{
	[SerializeField] TMP_Dropdown resolutionDropDown;
	[SerializeField] TextMeshProUGUI countdownText;
	[SerializeField] GameObject confirmPanel;
	[SerializeField] Button applyButton;
	[SerializeField] Button backButton;
	[SerializeField] Toggle windowed;
	[SerializeField] Toggle maxWindowed;
	[SerializeField] Toggle fullscreen;
	List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
	FullScreenMode oldScreenMode;
	FullScreenMode newScreenMode;
	Resolution[] resolutions;
	Resolution currentRes;
	Resolution newRes;
	int oldWidth;
	int oldHeight;
	int oldValue;
	int countdown = 15;
	float timer;
	bool changedOptions;
	bool toggleIsOn;


	void Awake()
	{
		Screen.fullScreenMode = FullScreenMode.Windowed;
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
		int currentWidth = Screen.width;
		int currentHeight = Screen.height;
		string currentRes = $"{currentWidth}x{currentHeight}";
		for (int i = 0; i < resolutionDropDown.options.Count; i++)
		{
			if (resolutionDropDown.options[i].text == currentRes)
			{
				int value = i;
				resolutionDropDown.value = value;
				oldValue = value;
				return;
			}
		}
	}

	private void Start()
	{
		countdownText.text = countdown.ToString();
		applyButton.interactable = false;
		confirmPanel.SetActive(false);
		newScreenMode = Screen.fullScreenMode;
	}

	private void Update()
	{
		if (changedOptions)
		{
			timer += Time.deltaTime;
			if (timer >= 1f)
			{
				countdown--;
				timer = 0f;
				countdownText.text = countdown.ToString();
			}

			if (countdown <= 0)
			{
				DontKeepChanges();
			}
		}
	}

	public void ChangeOptions()
	{
		applyButton.interactable = false;
		backButton.interactable = false;
		resolutionDropDown.interactable = false;
		confirmPanel.SetActive(true);

		oldWidth = Screen.width;
		oldHeight = Screen.height;
		oldScreenMode = Screen.fullScreenMode;
		newRes = resolutions[resolutionDropDown.value];
		Screen.SetResolution(newRes.width, newRes.height, newScreenMode, 0);
		changedOptions = true;
	}

	public void CheckIfToggleIsActive(Toggle toggle)
	{
		if (toggle.isOn == false) { toggleIsOn = false; }
		else { toggleIsOn = true; }
	}

	public void ChangeScreenMode(int index)
	{
		if (toggleIsOn)
		{
			switch (index)
			{
				case 0:
					newScreenMode = FullScreenMode.Windowed;
					resolutionDropDown.interactable = true;
					break;
				case 1:
					newScreenMode = FullScreenMode.FullScreenWindow;
					resolutionDropDown.interactable = true;
					break;
				default:
					break;
			}
			applyButton.interactable = true;
		}
	}

	public void KeepChanges()
	{
		changedOptions = false;
		timer = 0f;
		countdown = 15;
		countdownText.text = countdown.ToString();
		applyButton.interactable = false;
		backButton.interactable = true;
		resolutionDropDown.interactable = true;
		confirmPanel.SetActive(false);
		transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{Screen.width}x{Screen.height} \n {resolutionDropDown.options[resolutionDropDown.value].text}";
	}

	public void DontKeepChanges()
	{
		changedOptions = false;
		timer = 0f;
		countdown = 15;
		countdownText.text = countdown.ToString();
		Screen.SetResolution(oldWidth, oldHeight, oldScreenMode, 0);
		resolutionDropDown.value = oldValue;
		applyButton.interactable = false;
		backButton.interactable = true;
		resolutionDropDown.interactable = true;
		confirmPanel.SetActive(false);
	}
}
