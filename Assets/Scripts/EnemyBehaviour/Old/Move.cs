using UnityEngine;

namespace David
{
	public class Move : MonoBehaviour
	{
		EnemyAI enemyAI;


		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
		}

		public void Execute(Vector3 nextNavPoint)
		{
			float distance = (transform.position - nextNavPoint).magnitude;
			if (distance <= 1f)
			{
				enemyAI.waiting = true;
			}
			enemyAI.agent.SetDestination(nextNavPoint);
		}
	}
}
