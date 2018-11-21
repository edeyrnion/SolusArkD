using UnityEngine;

namespace David
{
	public class Wait : MonoBehaviour
	{
		public float WaitTime;
		EnemyAI enemyAI;
		Search search;
		int nextNavPointNumber = 0;
		float time;


		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
			search = GetComponent<Search>();
		}

		public void Execute()
		{
			time += Time.deltaTime;
			if (time >= WaitTime)
			{
				time = 0f;
				enemyAI.lookingAtTarget = false;
				if (nextNavPointNumber == enemyAI.navPoints.Count - 1) { nextNavPointNumber = 0; }
				else if (!search.goToLastPoint) { nextNavPointNumber++; }
				enemyAI.nextNavPoint = enemyAI.navPoints[nextNavPointNumber].transform.position;
				search.goToLastPoint = false;
				enemyAI.waiting = false;
			}
		}
	}
}