using UnityEngine;

namespace David
{
	public class GameManager : MonoBehaviour
	{
		public GameState GameState = GameState.Game;
	}

	public enum GameState { Game, PauseMenu }
}
