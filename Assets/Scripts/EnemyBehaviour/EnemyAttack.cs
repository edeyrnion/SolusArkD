using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyAttack : MonoBehaviour
	{
		[SerializeField] float waitTime;
		EnemyManager manager;
		NavMeshAgent agent;
		Vector3 targetPos;
		bool wait;


		public void OnStateEnter()
		{
			agent.isStopped = true;
			wait = false;
		}

		void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			if (manager.CurrentState == State.Attacking)
			{
				targetPos = manager.Player.transform.position;
				float distance = (transform.position - targetPos).magnitude;
				if (distance <= manager.AttackRadius)
				{
					agent.velocity = Vector3.zero;
					agent.isStopped = true;
					manager.BanditController.Attack();
				}
				else if (distance >= manager.AttackRadius * 1.5f)
				{
					print("test");
					manager.ChangeState(State.Following);
				}
			}
		}
	}
}
