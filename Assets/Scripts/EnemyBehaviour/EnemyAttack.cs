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
				if (distance <= manager.AttackRadius * 1.5f) { Attack(); return; }
				else
				{
					print("test");
					manager.ChangeState(State.Following);				
				}
			}
		}

		void Attack()
		{
			agent.velocity = Vector3.zero;
			agent.isStopped = true;
			manager.BanditController.Attack();
		}	
	}
}
