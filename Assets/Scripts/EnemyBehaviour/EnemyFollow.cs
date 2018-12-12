using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyFollow : MonoBehaviour
	{
		[SerializeField] float turnSpeed;
		[SerializeField] float waitTime;
		EnemyManager manager;
		NavMeshAgent agent;
		Vector3 targetPos;
		float distance;
		public bool LookingAtTarget;
		bool wait;
		bool breakOutOfCoroutine;
		IEnumerator coroutine;

		public void OnStateEnter()
		{			
			agent.speed = manager.Stats.WalkingSpeed * 1.75f;
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
				targetPos = manager.Player.transform.position;
				distance = (transform.position - targetPos).magnitude;
				NavMeshHit hit;
				bool hidden = !agent.Raycast(targetPos, out hit);
				if (wait && !breakOutOfCoroutine && distance <= manager.AlertRadius && hidden)
				{
					print("break");
					breakOutOfCoroutine = true;
					LookingAtTarget = false;
					wait = false;
				}
				if (!LookingAtTarget) { LookAtTarget(); }
				else if (!wait) { Follow(); }
			}
		}

		void LookAtTarget()
		{
			NavMeshPath path = new NavMeshPath();
			agent.CalculatePath(targetPos, path);
			if (path.corners.Length == 0)
			{
				Debug.LogError("Path not correcly calculated!");
				return;
			}
			Vector3 targetDir = path.corners[1] - transform.position;
			float step = turnSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) <= 0.3f)
			{
				LookingAtTarget = true;
				agent.isStopped = false;
			}
		}

		void Follow()
		{
			if (manager.BanditController != null)
			{
				manager.BanditController.Move(agent.velocity.magnitude);
			}
			targetPos = manager.Player.transform.position;
			agent.SetDestination(targetPos);
			if (distance <= manager.AttackRadius) { manager.ChangeState(State.Attacking); }
			else if (distance >= manager.AlertRadius * 1.5f)
			{
				agent.isStopped = true;
				wait = true;
				coroutine = Wait(waitTime);
				StartCoroutine(coroutine);
			}
		}

		IEnumerator Wait(float waitTime)
		{
			if (breakOutOfCoroutine) { breakOutOfCoroutine = false; yield break; }
			yield return new WaitForSeconds(waitTime);
			if (!manager.Detected) { manager.ChangeState(State.Patroling); }
			else
			{
				LookingAtTarget = false;
				wait = false;
			}
		}
	}
}
