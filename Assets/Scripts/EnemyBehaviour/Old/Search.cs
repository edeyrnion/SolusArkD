using UnityEngine;

namespace David
{
	public class Search : MonoBehaviour
	{
		public float WaitTime;
		public bool goToLastPoint;
		EnemyAI enemyAI;
		DetectPlayer detectPlayer;
		bool searching;
		float time;


		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
			detectPlayer = GetComponent<DetectPlayer>();
		}

		public void Execute()
		{
			if (!searching)
			{
				goToLastPoint = true;
				enemyAI.agent.isStopped = true;
				searching = true;
				enemyAI.waiting = false;
			}
			if (enemyAI.agent.velocity.magnitude <= 0.1f)
			{
				if (detectPlayer.spottedPlayer)
				{
					enemyAI.attacking = true;
					enemyAI.agent.isStopped = false;
					return;
				}

				time += Time.deltaTime;
				if (time >= WaitTime)
				{
					time = 0f;
					if (detectPlayer.Target == null)
					{
						enemyAI.alerted = false;
						searching = false;
						enemyAI.waiting = true;
						enemyAI.agent.isStopped = false;
					}
					else
					{
						enemyAI.nextNavPoint = detectPlayer.Target.transform.position + Random.insideUnitCircle.ToVector3() * 5;
						enemyAI.agent.isStopped = false;
					}
				}
			}
		}
	}
}