using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

namespace David
{
	public class EnemyStates : MonoBehaviour
	{
		[SerializeField] EnemyNavPoints navigation;
		[SerializeField] GameObject player;
		[SerializeField] float alertRadius;
		[SerializeField] float detectionRadius;
		[SerializeField] bool visualize;
		[SerializeField] bool alerted;
		[SerializeField] bool spottedPlayer;
		NavMeshAgent agent;
		List<GameObject> navPoints;
		GameObject target;
		public int nextNavPointNumber;
		Color color = Color.green;
		Vector3 nextNavPoint;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			navPoints = navigation.NavPoints;
			nextNavPoint = navPoints[0].transform.position;
			nextNavPointNumber = 0;
			visualize = true;
		}

		private void Update()
		{
			DetectPlayer();
			if (alerted) { Search(); }
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
				else
				{
					target = null;
					color = Color.green;
				}
			}			
		}

		void Search()
		{

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
