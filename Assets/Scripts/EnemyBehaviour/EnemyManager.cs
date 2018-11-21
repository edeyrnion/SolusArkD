using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace David
{
	public class EnemyManager : MonoBehaviour
	{
		public float AlertRadius;
		[SerializeField] float detectionRadius;
		public GameObject Player;
		public State CurrentState = State.Patroling;
		public bool Visualize = true;
		public bool Detected;
		public bool Following;
		EnemyMove enemyMove;
		EnemyInvestigate enemyInvestigate;
		EnemyFollow enemyFollow;
		NavMeshAgent agent;
		Color color = Color.green;


		private void Start()
		{
			enemyMove = GetComponent<EnemyMove>();
			enemyInvestigate = GetComponent<EnemyInvestigate>();
			enemyFollow = GetComponent<EnemyFollow>();
			agent = GetComponent<NavMeshAgent>();
		}

		private void Update()
		{
			CheckIfPlayerIsDetected();
			Debug.DrawLine(transform.position, agent.destination);
		}

		private void CheckIfPlayerIsDetected()
		{
			Vector3 myPos = transform.position;
			Vector3 playerPos = Player.transform.position;
			float distanceToPlayer = (myPos - playerPos).magnitude;
			if (distanceToPlayer > AlertRadius)
			{
				color = Color.green;
				Detected = false;
				return;
			}
			NavMeshHit hit;
			if (agent.Raycast(Player.transform.position, out hit)) { Detected = false; return; }
			color = Color.yellow;
			if (CurrentState != State.Investigating && !Following) { ChangeState(State.Investigating); }
			Detected = true;
		}

		public void ChangeState(State state)
		{
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
					break;
				default:
					break;
			}
			CurrentState = state;
		}

		private void OnDrawGizmos()
		{
			if (Visualize)
			{
				Gizmos.color = color;
				Gizmos.DrawLine(transform.position, Player.transform.position);
				Handles.color = Color.yellow;
				Handles.DrawWireDisc(transform.position, Vector3.up, AlertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, detectionRadius);
			}
		}
	}

	public enum State { Patroling, Investigating, Following, Attacking }

}