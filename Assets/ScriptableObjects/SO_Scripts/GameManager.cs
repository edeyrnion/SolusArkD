using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager", order = 4)]
	public class GameManager: ScriptableObject
	{
		public GameState GameState;


		public void ChangeState(GameState state)
		{
			GameState = state;
		}
	}

	public enum GameState { Game, PauseMenu } 
}
