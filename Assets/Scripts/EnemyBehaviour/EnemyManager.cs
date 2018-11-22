using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace David
{
	public class EnemyManager : MonoBehaviour
	{
		public State CurrentState = State.Patroling;
		public GameObject Player;
		public Color Color = Color.green;
		public float AlertRadius;
		public float DetectionRadius;
		public float AttackRadius;
		public bool Visualize = true;
		public bool Detected;
		EnemyMove enemyMove;
		EnemyInvestigate enemyInvestigate;
		EnemyFollow enemyFollow;
		EnemyAttack enemyAttack;
		NavMeshAgent agent;


		private void Start()
		{
			enemyMove = GetComponent<EnemyMove>();
			enemyInvestigate = GetComponent<EnemyInvestigate>();
			enemyFollow = GetComponent<EnemyFollow>();
			enemyAttack = GetComponent<EnemyAttack>();
			agent = GetComponent<NavMeshAgent>();
		}

		private void Update()
		{
			Debug.DrawLine(transform.position, agent.destination);
			Vector3 myPos = transform.position;
			Vector3 playerPos = Player.transform.position;
			float distanceToPlayer = (myPos - playerPos).magnitude;
			if (distanceToPlayer > AlertRadius)
			{
				Color = Color.green;
				Detected = false;
				return;
			}
			NavMeshHit hit;
			if (agent.Raycast(Player.transform.position, out hit)) { Detected = false; return; }
			Detected = true;
		}

		public void DealDamage(int damage)
		{
			Debug.Log($"Dealt {damage} points of damage!");
		}

		public void ChangeState(State state)
		{
			CurrentState = state;
			switch (state)
			{
				case State.Patroling:
					enemyMove.OnStateEnter();
					break;
				case State.Investigating:
					enemyInvestigate.OnStateEnter();
					break;
				case State.Following:
					enemyFollow.OnStateEnter();
					break;
				case State.Attacking:
					enemyAttack.OnStateEnter();
					break;
				default:
					break;
			}
		}

		private void OnDrawGizmos()
		{
			if (Visualize)
			{
				Gizmos.color = Color;
				Gizmos.DrawLine(transform.position, Player.transform.position);
				Handles.color = Color.yellow;
				Handles.DrawWireDisc(transform.position, Vector3.up, AlertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, DetectionRadius);
			}
		}
	}

	public enum State { Patroling, Investigating, Following, Attacking }

}