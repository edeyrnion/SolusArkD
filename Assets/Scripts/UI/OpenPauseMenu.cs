using UnityEngine;
using UnityEngine.SceneManagement;

namespace David
{
	public class OpenPauseMenu : MonoBehaviour
	{
		[SerializeField] GameEvent optionsEvent;
		[SerializeField] GameEvent audioEvent;
		[SerializeField] GameObject pausePanel;
		[SerializeField] GameObject optionsPanel;
		public bool scale = false;
		public bool active = true;


		private void Update()
		{

			int index = SceneManager.GetActiveScene().buildIndex;
			if (index == 2 && Input.GetButtonDown("Cancel"))
			{
				audioEvent.Raise();
				if (optionsPanel.transform.localPosition == Vector3.zero) { optionsEvent.Raise(); return; }
				if (scale) { Time.timeScale = 1; }
				else if (!scale) { Time.timeScale = 0; }

				pausePanel.SetActive(active);
				scale = !scale;
				active = !active;
			}
		}
	}
}
