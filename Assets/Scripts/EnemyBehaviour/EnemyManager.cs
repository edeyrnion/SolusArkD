using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

namespace David
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] float alertRadius;
		[SerializeField] float detectionRadius;
		public GameObject Player;
		public State CurrentState = State.Patroling;
		public bool Visualize = true;
		public bool Detected;
		public bool Attacking;
		EnemyMove enemyMove;
		EnemyInvestigate enemyInvestigate;
		NavMeshAgent agent;
		Color color = Color.green;


		private void Start()
		{
			enemyMove = GetComponent<EnemyMove>();
			enemyInvestigate = GetComponent<EnemyInvestigate>();
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
			if (distanceToPlayer > alertRadius)
			{
				color = Color.green;
				Detected = false;
				return;
			}
			NavMeshHit hit;
			if (agent.Raycast(Player.transform.position, out hit)) { Detected = false; return; }
			color = Color.yellow;
			if (CurrentState != State.Investigating && !Attacking) { ChangeState(State.Investigating); }
			Detected = true;
		}

		public void ChangeState(State state)
		{
			switch (state)
			{
				case State.Patroling:
					agent.SetDestination(enemyMove.CurrentNavPoint);
					enemyMove.LookingAtTarget = false;
					enemyMove.IsMoving = false;
					agent.isStopped = false;
					break;
				case State.Investigating:
					enemyInvestigate.LookingAtTarget = false;
					enemyInvestigate.Investigating = false;
					agent.isStopped = true;
					break;
				case State.Attacking:
					agent.isStopped = true;
					print("Attacking");
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
				Handles.DrawWireDisc(transform.position, Vector3.up, alertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, detectionRadius);
			}
		}
	}

	public enum State { Patroling, Investigating, Attacking }

}