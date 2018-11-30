using UnityEngine;
using UnityEngine.SceneManagement;

namespace David
{
	public class ChangeScene : GameEventListener
	{
		[SerializeField] GameObject mainPanel;
		[SerializeField] GameObject pausePanel;
		[SerializeField] int sceneIndex;
		[SerializeField] bool active;


		public void LoadGameScene()
		{
			mainPanel.SetActive(active);
			pausePanel.SetActive(false);
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
