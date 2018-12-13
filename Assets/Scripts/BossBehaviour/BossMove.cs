using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class BossMove : MonoBehaviour
	{
		BossManager manager;
		NavMeshAgent agent;
		BossController controller;


		private void Start()
		{
			manager = GetComponent<BossManager>();
			controller = GetComponent<BossController>();
			agent = manager.agent;
		}

		private void Update()
		{
			if (manager.state != BossState.Follow) { return; }
			manager.CheckDistanceToTarget();
			agent.SetDestination(manager.target.transform.position);
			controller.Move(agent.velocity.magnitude);
		}
	}
}
