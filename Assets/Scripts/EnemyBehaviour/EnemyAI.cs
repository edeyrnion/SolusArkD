using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

namespace David
{
	public class EnemyAI : MonoBehaviour
	{
		[SerializeField] EnemyNavPoints navigation;
		[SerializeField] GameObject player;
		[SerializeField] float turnSpeed;
		[SerializeField] float waitTime;
		[SerializeField] float searchTimer;
		[SerializeField] float alertRadius;
		[SerializeField] float detectionRadius;
		[SerializeField] bool alerted;
		[SerializeField] bool spottedPlayer;
		NavMeshAgent agent;
		GameObject target;
		List<GameObject> navPoints;
		public int nextNavPointNumber;
		Vector3 nextNavPoint;
		Vector3 targetDir;
		float timer;
		float attackTimer;
		public bool lookingAtTarget;
		bool goToLastPoint;
		bool wait;
		bool search;
		bool attack;
		bool stop;
		bool visualize;
		Color color = Color.green;


		void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			navPoints = navigation.NavPoints;
			nextNavPoint = navPoints[0].transform.position;
			nextNavPointNumber = 0;
			visualize = true;
		}

		void Update()
		{
			DetectPlayer();
			if (attack) { Attack(); }
			if (alerted && !attack) { Search(); }
			if (wait) { Wait(); }
			else if (!stop)
			{
				if (!lookingAtTarget)
				{
					NavMeshHit hit;
					if (agent.Raycast(nextNavPoint, out hit))
					{
						NavMeshPath path = new NavMeshPath();
						agent.CalculatePath(nextNavPoint, path);
						LookAtNewTarget(path.corners[1]);
					}
					else { LookAtNewTarget(nextNavPoint); }
				}
				else { Move(nextNavPoint); }
			}
		}

		void Move(Vector3 nextNavPoint)
		{
			float distance = (transform.position - nextNavPoint).magnitude;
			if (distance <= 1f)
			{
				wait = true;
			}
			agent.SetDestination(nextNavPoint);
		}

		public void Wait()
		{
			timer += Time.deltaTime;
			if (timer >= waitTime)
			{
				lookingAtTarget = false;
				if (nextNavPointNumber == navPoints.Count - 1) { nextNavPointNumber = 0; }
				else if (!goToLastPoint) { nextNavPointNumber++; }
				nextNavPoint = navPoints[nextNavPointNumber].transform.position;
				timer = 0f;
				goToLastPoint = false;
				wait = false;
			}
		}

		void Search()
		{
			if (!search)
			{
				goToLastPoint = true;
				agent.isStopped = true;
				search = true;
				wait = false;
			}
			if (agent.velocity.magnitude <= 0.1f)
			{
				if (spottedPlayer)
				{
					attack = true;
					agent.isStopped = false;
					return;
				}

				searchTimer += Time.deltaTime;
				if (searchTimer >= waitTime)
				{
					searchTimer = 0f;
					if (target == null)
					{
						alerted = false;
						search = false;
						wait = true;
						agent.isStopped = false;
					}
					else
					{
						nextNavPoint = target.transform.position + Random.insideUnitCircle.ToVector3() * 5;
						agent.isStopped = false;
					}
				}
			}
		}

		void Attack()
		{
			nextNavPoint = target.transform.position;
			float distance = (transform.position - nextNavPoint).magnitude;
			if (distance <= 2f)
			{
				stop = true;
				agent.isStopped = true;
				attackTimer += Time.deltaTime;
				if (attackTimer >= 2f)
				{
					attackTimer = 0f;
					print("Attack!");
				}
			}
			else if (distance >= 2f)
			{
				stop = false;
				agent.isStopped = false;
			}
			if (distance >= alertRadius * 1.2f)
			{
				attack = false;
				spottedPlayer = false;
				agent.isStopped = false;
				stop = false;
			}
			
		}

		void LookAtNewTarget(Vector3 lookTarget)
		{
			targetDir = lookTarget - transform.position;
			Debug.DrawRay(transform.position, targetDir, Color.red);
			float step = turnSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) <= 0.1f) { lookingAtTarget = true; }
		}

		void DetectPlayer()
		{
			Vector3 myPos = transform.position;
			Vector3 playerPos = player.transform.position;
			float distanceToPlayer = (myPos - playerPos).magnitude;
			if (distanceToPlayer > alertRadius)
			{
				target = null;
				color = Color.green;
				return;
			}
			Ray ray = new Ray(myPos, playerPos - myPos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, alertRadius))
			{
				target = hit.transform.gameObject;
				if (target.CompareTag("Player"))
				{
					color = Color.yellow;
					alerted = true;
					if (Physics.Raycast(ray, out hit, detectionRadius))
					{
						color = Color.red;
						spottedPlayer = true;
					}
				}
				else { color = Color.green; }
			}
		}

		private void OnDrawGizmos()
		{
			if (visualize)
			{
				Gizmos.color = color;
				Gizmos.DrawLine(transform.position, player.transform.position);
				Handles.color = Color.yellow;
				Handles.DrawWireDisc(transform.position, Vector3.up, alertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, detectionRadius);
			}
		}
	}
}
