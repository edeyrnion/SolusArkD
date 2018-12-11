using UnityEngine;
using UnityEngine.SceneManagement;

namespace David
{
	public class OpenPauseMenu : MonoBehaviour
	{
		[SerializeField] GameManager manager;
		[SerializeField] GameEvent optionsEvent;
		[SerializeField] GameEvent audioEvent;
		[SerializeField] GameObject pausePanel;
		[SerializeField] GameObject optionsPanel;
		[SerializeField] GameObject endScreenPanel;

		public bool scale = false;
		public bool active = true;


		private void Start()
		{
			manager.ChangeState(GameState.Game);
		}

		private void Update()
		{
			if (endScreenPanel.activeSelf == true) { return; }
			int index = SceneManager.GetActiveScene().buildIndex;
			if (index == 2 && Input.GetButtonDown("Cancel"))
			{
				if (manager.GameState == GameState.Game) { manager.ChangeState(GameState.PauseMenu); Cursor.visible = true; }
				else if (manager.GameState == GameState.PauseMenu) { manager.ChangeState(GameState.Game); Cursor.visible = false; }
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
