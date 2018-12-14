using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class Dog : MonoBehaviour
	{
		[SerializeField] GameObject player;

		NavMeshAgent agent;
		Animator animator;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
		}

		void Update()
		{
			agent.SetDestination(player.transform.position);
            animator.SetFloat("Speed", agent.velocity.magnitude);
		}
	}
}
