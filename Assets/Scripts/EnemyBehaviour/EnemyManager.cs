using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace David
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] EnemyStats stats;

		public State CurrentState = State.Patroling;
		public GameObject Player;
		public Color Color = Color.green;
		public int Health;
		public int Damage;
		public float AttackWaitTime;
		public float AlertRadius;
		public float DetectionRadius;
		public float AttackRadius;
		public bool Visualize = true;
		public bool Detected;
		public bool OtherEnemyIsAlerted;

		EnemyMove enemyMove;
		EnemyInvestigate enemyInvestigate;
		EnemyFollow enemyFollow;
		EnemyAttack enemyAttack;
		NavMeshAgent agent;


		private void Awake()
		{
			enemyMove = GetComponent<EnemyMove>();
			enemyInvestigate = GetComponent<EnemyInvestigate>();
			enemyFollow = GetComponent<EnemyFollow>();
			enemyAttack = GetComponent<EnemyAttack>();
			agent = GetComponent<NavMeshAgent>();

			Health = stats.Health;
			Damage = stats.Damage;
			AttackWaitTime = stats.AttackWaitTime;
			DetectionRadius = stats.DetectionRadius;
			AlertRadius = stats.AlertRadius;
			agent.speed = stats.WalkingSpeed;
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
#if UNITY_EDITOR
				Gizmos.color = Color;
				Gizmos.DrawLine(transform.position, Player.transform.position);
				Handles.color = Color.yellow;
				Handles.DrawWireDisc(transform.position, Vector3.up, AlertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, DetectionRadius);
#endif
			}
		}
	}

	public enum State { Patroling, Investigating, Following, Attacking }
}