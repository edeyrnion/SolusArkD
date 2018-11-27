using UnityEngine;

namespace David
{
	public class ExitGame : GameEventListener
	{
		public void CloseGame()
		{
			print("Exiting Game");
			Application.Quit();
		}
	}
}
