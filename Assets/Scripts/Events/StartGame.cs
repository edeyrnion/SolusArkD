using UnityEngine;
using UnityEngine.SceneManagement;

namespace David
{
	public class StartGame : GameEventListener
	{
		[SerializeField] int sceneIndex;


		public void LoadGameScene()
		{
			SceneManager.LoadScene(sceneIndex);
		}
	}
}
