using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyInvestigate : MonoBehaviour
	{
		[SerializeField] float turnSpeed;
		[SerializeField] float waitTime;
		[SerializeField] float investigationOffset;
		EnemyManager manager;
		NavMeshAgent agent;
		IEnumerator coroutine;
		Vector3 targetPos;
		Vector3 poitOfInvestigation;
		public bool LookingAtTarget;
		public bool Investigated;
		bool breakOutOfCoroutine;


		public void OnStateEnter()
		{
			print(manager.CurrentState);
			agent.velocity = Vector3.zero;
			agent.isStopped = true;
			LookingAtTarget = false;
			Investigated = false;
		}

		void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			if (manager.CurrentState == State.Investigating)
			{
				targetPos = manager.Player.transform.position;
				float distanceToPlayer = (transform.position - targetPos).magnitude;
				if (distanceToPlayer <= manager.DetectionRadius)
				{
					breakOutOfCoroutine = true;
					manager.ChangeState(State.Following);
				}
				if (!LookingAtTarget) { LookAtTarget(); }
				float distance = (transform.position - poitOfInvestigation).magnitude;
				if (distance <= manager.DetectionRadius) { manager.ChangeState(State.Following); return; }
				if (Investigated)
				{
					if (distance <= 0.1f)
					{
						if (manager.Detected) { manager.ChangeState(State.Following); }
						else
						{
							coroutine = Wait(waitTime * 1.5f);
							StartCoroutine(coroutine);
						}
					}
				}
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
				coroutine = Wait(waitTime);
				StartCoroutine(coroutine);
			}
		}

		void Investigate()
		{
			poitOfInvestigation = targetPos + Random.insideUnitCircle.ToVector3() * investigationOffset;
			agent.SetDestination(poitOfInvestigation);
			agent.isStopped = false;
			Investigated = true;
		}

		IEnumerator Wait(float waitTime)
		{
			if (breakOutOfCoroutine) { breakOutOfCoroutine = false; yield break; }
			yield return new WaitForSeconds(waitTime);
			if (!Investigated) { Investigate(); }
			else { manager.ChangeState(State.Patroling); }
		}
	}
}
