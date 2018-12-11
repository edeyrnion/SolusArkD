using UnityEngine;
using UnityEngine.SceneManagement;

namespace David
{
	public class ChangeScene : GameEventListener
	{
		[SerializeField] GameObject mainPanel;
		[SerializeField] GameObject pausePanel;
		[SerializeField] GameObject endScreenPanel;
		[SerializeField] int sceneIndex;
		[SerializeField] bool active;

		[SerializeField] OpenPauseMenu pauseMenu;

		public void LoadGameScene()
		{
			endScreenPanel.SetActive(false);
			mainPanel.SetActive(active);
			pausePanel.SetActive(false);
			SceneManager.LoadScene(sceneIndex);
			pauseMenu.active = true;
			pauseMenu.scale = false;
			Time.timeScale = 1;
		}
	}
}
