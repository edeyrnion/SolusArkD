using UnityEngine;

namespace David
{
	public class EnemyStates : MonoBehaviour
	{
		EnemyAI enemyAI;


		void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
		}

		//public void ExecuteState(State state)
		//{
		//	switch (state)
		//	{
		//		case State.Idle:					
		//			break;
		//		case State.Patrol:
		//			Patrol();
		//			break;
		//		case State.Alert:
		//			Alert();
		//			break;
		//		case State.Search:
		//			Search();
		//			break;
		//		case State.Follow:
		//			Follow();
		//			break;
		//		case State.Attack:
		//			Attack();
		//			break;
		//		default:
		//			break;
		//	}
		//}
	}

	public enum State { Idle, Patrol, Alert, Search, Follow, Attack }
}
