using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPauseMenu : MonoBehaviour
{
	[SerializeField] GameObject pausePanel;
	bool scale = false;
	bool active = true;


	private void Update()
	{
		int index = SceneManager.GetActiveScene().buildIndex;
		if (index == 2 && Input.GetButtonDown("Cancel"))
		{
			if (scale) { Time.timeScale = 1; }
			else if (!scale) { Time.timeScale = 0; }

			pausePanel.SetActive(active);
			scale = !scale;
			active = !active;
		}		
	}
}
