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
		[SerializeField] float waitTime;
		[SerializeField] float searchTimer;
		[SerializeField] float alertRadius;
		[SerializeField] float detectionRadius;
		[SerializeField] bool alerted;
		EnemyStates enemyStates;
		NavMeshAgent agent;
		GameObject target;
		Vector3 lookTarget;
		List<GameObject> navPoints;
		public int nextNavPointNumber;
		Vector3 nextNavPoint;
		Vector3 currentPathPoint;
		float timer;
		bool wait;
		bool search;
		bool visualize;
		Color color = Color.green;


		void Start()
		{
			enemyStates = GetComponent<EnemyStates>();
			agent = GetComponent<NavMeshAgent>();
			navPoints = navigation.NavPoints;
			nextNavPoint = navPoints[0].transform.position;
			nextNavPointNumber = 0;
			visualize = true;
		}

		void Update()
		{			
			DetectPlayer();
			if (alerted) { Search(); }
			if (wait) { Wait(); }
			else { Move(nextNavPoint); }
		}

		void Move(Vector3 nextNavPoint)
		{
			float distance = (transform.position - nextNavPoint).magnitude;
			if (distance <= 1f) { wait = true; }
			agent.SetDestination(nextNavPoint);
		}

		public void Wait()
		{
			timer += Time.deltaTime;
			if (timer >= waitTime)
			{
				if (nextNavPointNumber == navPoints.Count - 1) { nextNavPointNumber = 0; }
				else { nextNavPointNumber++; }
				nextNavPoint = navPoints[nextNavPointNumber].transform.position;
				timer = 0f;
				wait = false;
			}
		}

		void Search()
		{
			if (!search)
			{
				lookTarget = target.transform.position;				
				search = true;
				agent.isStopped = true;		
				wait = false;
				IEnumerator coroutine = SearchWait(searchTimer);
				StartCoroutine(coroutine);
			}			
			transform.LookAt(lookTarget);
		}

		IEnumerator SearchWait(float searchTimer)
		{			
			yield return new WaitForSeconds(searchTimer);			
			if(target == null)
			{
				alerted = false;
				search = false;
				wait = true;
				agent.isStopped = false;
			}
			else
			{				
				currentPathPoint = nextNavPoint;
				nextNavPoint = target.transform.position + Random.insideUnitCircle.ToVector3() * 3;
				agent.isStopped = false;
				search = false;
			}
		}

		void DetectPlayer()
		{			
			Vector3 myPos = transform.position;
			Vector3 playerPos = player.transform.position;
			Ray ray = new Ray(myPos, playerPos - myPos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, alertRadius))
			{
				target = hit.transform.gameObject;
				if (target.CompareTag("Player"))
				{
					color = Color.yellow;
					alerted = true;
					if (Physics.Raycast(ray, out hit, detectionRadius)) { color = Color.red; }					
				}
				else
				{
					target = null;
					color = Color.green;					
				}
			}
			else
			{
				target = null;				
				color = Color.green;				
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
