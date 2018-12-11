using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyMove : MonoBehaviour
	{
		[SerializeField] EnemyNavPoints enemyNavPoints;
		[SerializeField] float stopingDistance;
		[SerializeField] float waitTime;
		[SerializeField] float turnSpeed;
		EnemyManager manager;
		NavMeshAgent agent;
		List<GameObject> navPoints = new List<GameObject>(8);
		public Vector3 CurrentNavPoint;
		int navPointNumber = 0;
		public bool IsMoving;
		public bool LookingAtTarget;

		public void OnStateEnter()
		{
			print(manager.CurrentState);
			agent.speed = manager.Stats.WalkingSpeed;
			LookingAtTarget = false;
			IsMoving = false;
		}

		private void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
			navPoints = enemyNavPoints.NavPoints;
			CurrentNavPoint = navPoints[navPointNumber].transform.position;
		}

		void Update()
		{
			if (manager.CurrentState == State.Patroling)
			{
				if (manager.BanditController != null)
				{
					manager.BanditController.Move(agent.velocity.magnitude);
				}
				if (manager.Detected) { CheckIfPlayerIsDetected(); }
				if (IsMoving)
				{
					float distance = (transform.position - CurrentNavPoint).magnitude;
					if (distance <= stopingDistance)
					{
						IsMoving = false;
						IEnumerator coroutine = Wait(waitTime);
						StartCoroutine(coroutine);
					}
				}
				if (!LookingAtTarget) { LookAtTarget(); }
			}
		}

		private void CheckIfPlayerIsDetected()
		{
			if (manager.OtherEnemyIsAlerted) { manager.ChangeState(State.Following); }
			else
			{
				manager.Color = Color.yellow;
				manager.ChangeState(State.Investigating);
			}
		}

		void GetNextNavPoint()
		{
			if (navPointNumber == navPoints.Count - 1) { navPointNumber = 0; }
			else { navPointNumber++; }
			CurrentNavPoint = navPoints[navPointNumber].transform.position;
			LookingAtTarget = false;
		}

		void LookAtTarget()
		{
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(CurrentNavPoint, path);
			if (path.corners.Length == 0)
			{
				Debug.LogError("Path not correcly calculated!");
				return;
			}
			Vector3 targetDir = path.corners[1] - transform.position;
			Debug.DrawRay(transform.position, targetDir, Color.red);
			float step = turnSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) <= 0.3f)
			{
				LookingAtTarget = true;
				IsMoving = true;
				agent.SetDestination(CurrentNavPoint);
				if (agent.isStopped) { agent.isStopped = false; }
			}
		}

		IEnumerator Wait(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			GetNextNavPoint();
		}
	}
}
