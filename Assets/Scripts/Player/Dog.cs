using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class Dog : MonoBehaviour
	{
		[SerializeField] GameObject player;

		NavMeshAgent agent;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
		}

		void Update()
		{
			agent.SetDestination(player.transform.position);			
		}
	}
}
