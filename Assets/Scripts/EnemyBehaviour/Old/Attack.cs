using UnityEngine;

namespace David
{
	public class Attack : MonoBehaviour
	{
		public float WaitTime;
		EnemyAI enemyAI;
		DetectPlayer detectPlayer;
		float time;


		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
			detectPlayer = GetComponent<DetectPlayer>();
		}

		public void Execute()
		{			
			enemyAI.nextNavPoint = detectPlayer.Target.transform.position;
			float distance = (transform.position - enemyAI.nextNavPoint).magnitude;
			if (distance <= 2f)
			{
				enemyAI.stop = true;
				enemyAI.agent.isStopped = true;
				time += Time.deltaTime;
				if (time >= WaitTime)
				{
					time = 0f;
					print("Attack!");
				}
			}
			else if (distance >= 2f)
			{
				enemyAI.stop = false;
				enemyAI.agent.isStopped = false;
			}
			if (distance >= detectPlayer.alertRadius * 1.2f)
			{
				enemyAI.attacking = false;
				detectPlayer.spottedPlayer = false;
				enemyAI.agent.isStopped = false;
				enemyAI.stop = false;
			}
		}
	}
}