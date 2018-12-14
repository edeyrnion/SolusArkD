using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class Dog : MonoBehaviour
	{
		[SerializeField] GameObject player;

		NavMeshAgent agent;
		Animator animator;

        float lastTimer;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
		}

		void Update()
		{
			agent.SetDestination(player.transform.position);

            var currentTime = Time.time;
            if (Vector3.Distance(transform.position, player.transform.position) > 20f)
            {
                lastTimer = currentTime;
                agent.Warp(player.transform.position - Vector3.one + Vector3.up);
                agent.ResetPath();
            }

            agent.SetDestination(player.transform.position);

            animator.SetFloat("Speed", agent.velocity.magnitude);
		}
	}
}
