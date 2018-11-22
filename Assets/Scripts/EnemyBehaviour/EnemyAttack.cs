using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyAttack : MonoBehaviour
	{
		[SerializeField] int damage;		
		[SerializeField] float attackSpeed;
		[SerializeField] float waitTime;
		EnemyManager manager;
		NavMeshAgent agent;
		Vector3 targetPos;
		float timer;
		bool wait;


		public void OnStateEnter()
		{
			print(manager.CurrentState);
			agent.isStopped = true;
			timer = attackSpeed;
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
			if (timer >= attackSpeed)
			{
				timer = 0f;
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
