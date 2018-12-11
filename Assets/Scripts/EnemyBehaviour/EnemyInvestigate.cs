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
		Vector3 targetPos;
		Vector3 poitOfInvestigation;
		public bool LookingAtTarget;
		public bool Investigated;
		float distanceToPlayer;
		bool secondCycle;


		public void OnStateEnter()
		{
			print(manager.CurrentState);
			agent.velocity = Vector3.zero;
			agent.isStopped = true;
			LookingAtTarget = false;
			Investigated = false;
			secondCycle = false;
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
				if (manager.BanditController != null)
				{
					manager.BanditController.Move(agent.velocity.magnitude);
				}
				Vector3 playerPos = manager.Player.transform.position;
				distanceToPlayer = (transform.position - playerPos).magnitude;
				if (!LookingAtTarget) { LookAtTarget(); }
				if (distanceToPlayer <= manager.DetectionRadius) { manager.ChangeState(State.Following); return; }
				if (Investigated)
				{
					float distance = (transform.position - poitOfInvestigation).magnitude;
					if (distance <= 0.1f)
					{
						if (manager.Detected) { manager.ChangeState(State.Following); }
						else if (!secondCycle)
						{
							string test = "second";
							IEnumerator coroutine = Wait(waitTime * 1.5f, test);
							StartCoroutine(coroutine);
							secondCycle = true;
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
				string test = "first";
				IEnumerator coroutine = Wait(waitTime, test);
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

		IEnumerator Wait(float waitTime, string test)
		{
			print(test);
			if (distanceToPlayer <= manager.DetectionRadius)
			{
				manager.ChangeState(State.Following);
				yield break;
			}
			yield return new WaitForSeconds(waitTime);
			if (!Investigated) { Investigate(); }
			else if (manager.CurrentState == State.Investigating) manager.ChangeState(State.Patroling);
		}
	}
}
