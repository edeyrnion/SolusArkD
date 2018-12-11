using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace David
{
	public class EnemyManager : MonoBehaviour
	{
		public EnemyStats Stats;

		public State CurrentState = State.Patroling;
		public BanditController BanditController;
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
			BanditController = GetComponent<BanditController>();
			enemyMove = GetComponent<EnemyMove>();
			enemyInvestigate = GetComponent<EnemyInvestigate>();
			enemyFollow = GetComponent<EnemyFollow>();
			enemyAttack = GetComponent<EnemyAttack>();
			agent = GetComponent<NavMeshAgent>();

			Health = Stats.Health;
			Damage = Stats.Damage;
			AttackWaitTime = Stats.AttackWaitTime;
			DetectionRadius = Stats.DetectionRadius;
			AlertRadius = Stats.AlertRadius;
			agent.speed = Stats.WalkingSpeed;
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
			Player.GetComponent<PlayerManager>().Stats.Health -= damage;
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
				case State.Dead:
					enabled = false;
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

	public enum State { Patroling, Investigating, Following, Attacking, Dead }
}