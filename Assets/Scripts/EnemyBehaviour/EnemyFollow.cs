using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyFollow : MonoBehaviour
	{
		[SerializeField] float turnSpeed;
		[SerializeField] float stoppingDistance;
		[SerializeField] float waitTime;
		EnemyManager manager;
		NavMeshAgent agent;
		Vector3 targetPos;
		public bool LookingAtTarget;
		bool wait;

		public void OnStateEnter()
		{
			agent.isStopped = true;
			LookingAtTarget = false;
			wait = false;
		}

		private void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			if (manager.CurrentState == State.Following)
			{
				if (!LookingAtTarget) { LookAtTarget(); }
				else if (!wait) { Follow(); }
			}
		}

		void LookAtTarget()
		{
			targetPos = manager.Player.transform.position;
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(targetPos, path);
			Vector3 targetDir = path.corners[1] - transform.position;
			Debug.DrawRay(transform.position, targetDir, Color.red);
			float step = turnSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) <= 0.1f)
			{
				LookingAtTarget = true;
				agent.isStopped = false;
			}
		}

		void Follow()
		{
			targetPos = manager.Player.transform.position;
			agent.SetDestination(targetPos);
			float distance = (transform.position - targetPos).magnitude;
			if (distance <= stoppingDistance) { manager.ChangeState(State.Attacking); }
			else if (distance >= manager.AlertRadius * 1.5f)
			{
				agent.isStopped = true;
				wait = true;
				IEnumerator coroutine = Wait(waitTime);
				StartCoroutine(coroutine);
			}
		}

		IEnumerator Wait(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			if (!manager.Detected)
			{
				manager.Following = false;
				manager.ChangeState(State.Patroling);
			}
			else
			{
				agent.isStopped = false;
				wait = false;
			}
		}
	}
}
