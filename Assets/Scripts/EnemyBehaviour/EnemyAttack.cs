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
		int damage;		
		float attackWaitTime;
		float timer;
		bool wait;


		public void OnStateEnter()
		{
			print(manager.CurrentState);
			agent.isStopped = true;
			timer = attackWaitTime;
			wait = false;
		}

		void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
			damage = manager.Damage;			
			attackWaitTime = manager.AttackWaitTime;
		}

		void Update()
		{
			if (manager.CurrentState == State.Attacking)
			{
				targetPos = manager.Player.transform.position;
				float distance = (transform.position - targetPos).magnitude;
				if (distance <= manager.AttackRadius) { Attack(); }
				else if (!wait)
				{
					wait = true;
					IEnumerator coroutine = Wait(waitTime);
					StartCoroutine(coroutine);
				}
			}
		}

		void Attack()
		{
			agent.velocity = Vector3.zero;
			agent.isStopped = true;
			timer += Time.deltaTime;
			if (timer >= attackWaitTime)
			{
				timer = 0f;
				if (manager.BanditController != null)
				{
					manager.BanditController.Attack();
				}
				manager.DealDamage(damage);
			}
		}

		IEnumerator Wait(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			manager.ChangeState(State.Following);
			wait = false;
		}
	}
}
