using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class BossMove : MonoBehaviour
	{
		BossManager manager;
		NavMeshAgent agent;


		private void Start()
		{
			manager = GetComponent<BossManager>();
			agent = manager.agent;
		}

		private void Update()
		{
			if (manager.state != BossState.Follow) { return; }
			manager.CheckDistanceToTarget();
			agent.SetDestination(manager.target.transform.position);
		}
	}
}
