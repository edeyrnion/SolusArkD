using UnityEngine;
using UnityEngine.UI;

namespace David
{
	public class OptionsMenu : GameEventListener
	{
		[SerializeField] ResolutionSettings resSettings;
		[SerializeField] QualitySettings qSettings;
		[SerializeField] GameObject optionsPanel;
		[SerializeField] Button applyButton;

		Vector3 startPos;
		Vector3 targetPos;


		private void Start()
		{
			startPos = optionsPanel.transform.localPosition;
			targetPos = Vector3.zero;
			applyButton.interactable = false;
		}

		public void OpenOptionsMenu()
		{
			optionsPanel.transform.localPosition = targetPos;
			if (targetPos == Vector3.zero) { targetPos = startPos; }
			else if (targetPos == startPos) { targetPos = Vector3.zero; }
		}

		public void ApplyChanges()
		{
			UnityEngine.QualitySettings.SetQualityLevel(qSettings.qualityIndex);
			Screen.SetResolution(resSettings.resWidth, resSettings.resHeight, resSettings.fullScreen);
			applyButton.interactable = false;
		}
	}
}
