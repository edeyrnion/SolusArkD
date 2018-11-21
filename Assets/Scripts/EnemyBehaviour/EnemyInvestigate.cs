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
		public bool Investigating;


		void Start()
		{
			manager = GetComponent<EnemyManager>();
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			if (manager.CurrentState == State.Investigating)
			{
				if (!LookingAtTarget) { LookAtTarget(); }
				if (Investigating)
				{
					float distance = (transform.position - poitOfInvestigation).magnitude;
					if (distance <= 0.1f)
					{
						if (manager.Detected)
						{
							manager.Attacking = true;
							manager.ChangeState(State.Attacking);
						}
						else { manager.ChangeState(State.Patroling); }
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
				IEnumerator coroutine = Wait(waitTime);
				StartCoroutine(coroutine);
			}
		}

		void Investigate()
		{
			poitOfInvestigation = targetPos + Random.insideUnitCircle.ToVector3() * investigationOffset;
			agent.SetDestination(poitOfInvestigation);
			agent.isStopped = false;
			Investigating = true;
		}

		IEnumerator Wait(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			if (manager.Detected) { Investigate(); }
			else { manager.ChangeState(State.Patroling); }
		}
	}
}
