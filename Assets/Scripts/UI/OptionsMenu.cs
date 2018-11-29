using UnityEngine;
using UnityEngine.UI;

namespace David
{
	public class OptionsMenu : GameEventListener
	{
		[SerializeField] ResolutionSettings resSettings;
		[SerializeField] QualitySettings qSettings;
		[SerializeField] GameObject optionsPanel;
		[SerializeField] RectTransform endPos;
		[SerializeField] RectTransform activePos;
		[SerializeField] Button applyButton;

		[SerializeField] float speed;

		Vector3 startPos;
		Vector3 currentPos;
		Vector3 targetPos;

		float velocity;
		bool setActive;


		private void Start()
		{
			startPos = optionsPanel.transform.localPosition;
			currentPos = startPos;
			targetPos = activePos.localPosition;

			applyButton.interactable = false;
		}

		public void OpenOptionsMenu()
		{
			setActive = true;
		}

		private void Update()
		{
			if (setActive)
			{
				currentPos.x = Mathf.SmoothDamp(currentPos.x, targetPos.x, ref velocity, speed);
				optionsPanel.transform.localPosition = new Vector3(currentPos.x, currentPos.y, currentPos.z);
				if (currentPos.x >= targetPos.x - 0.1f)
				{
					GetNewPos();
					setActive = false;
				}
			}
		}

		void GetNewPos()
		{
			if (currentPos.x >= endPos.localPosition.x - 0.1f)
			{
				currentPos.x = startPos.x;
				targetPos.x = activePos.localPosition.x;
				optionsPanel.transform.localPosition = startPos;
			}
			else if (currentPos.x >= activePos.localPosition.x - 0.1f)
			{
				currentPos.x = activePos.localPosition.x;
				targetPos.x = endPos.localPosition.x;
			}
		}

		public void ApplyChanges()
		{
			UnityEngine.QualitySettings.SetQualityLevel(qSettings.qualityIndex);
			Screen.SetResolution(resSettings.resWidth, resSettings.resHeight, resSettings.fullScreen);
			applyButton.interactable = false;
		}
	}
}
