using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] EnemyNavPoints navigation;
	NavMeshAgent agent;
	List<GameObject> navPoints;
	public int nextNavPointNumber;
	Vector3 nextNavPoint;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		navPoints = navigation.navPoints;
		nextNavPoint = navPoints[0].transform.position;
		nextNavPointNumber = 0;
	}

	void Update()
	{
		Move();
	}

	private void Move()
	{
		float distance = (transform.position - nextNavPoint).magnitude;
		if (distance <= 0.5f)
		{
			if (nextNavPointNumber == navPoints.Count - 1) { nextNavPointNumber = 0; }
			else { nextNavPointNumber++; }
			nextNavPoint = navPoints[nextNavPointNumber].transform.position;
		}
		agent.SetDestination(nextNavPoint);
	}
}
