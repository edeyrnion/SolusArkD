using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class BossManager : GameEventListener
	{
		[SerializeField] GameObject[] pillars;
		[SerializeField] float chargeTimer;
		public float attackDistance;
		public float bossSpeed;

		public BossState state = BossState.Idle;

		public NavMeshAgent agent;
		public GameObject target;

		public bool Charging = true;

		float time;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			agent.speed = bossSpeed;
		}

		public void StartFight()
		{
			Debug.Log("Player in Boss Room. Starting Fight.");
			Charging = false;
			state = BossState.Follow;
		}

		private void Update()
		{
			if (!Charging)
			{
				time += Time.deltaTime;
				if (time >= chargeTimer)
				{
					time = 0f;
					agent.isStopped = true;
					state = BossState.Charge;
					Debug.Log("Start Charging");
					Charging = true;
				}
			}
		}

		public void CheckDistanceToTarget()
		{
			Vector3 myPos = transform.position;
			Vector3 targetPos = target.transform.position;
			var distance = (myPos - targetPos).sqrMagnitude;
			if (distance < attackDistance * attackDistance && state != BossState.Attack)
			{
				Debug.Log("Start Attacking");
				state = BossState.Attack;
				agent.velocity = Vector3.zero;
				agent.isStopped = true;
			}
			if (distance >= attackDistance * attackDistance && state != BossState.Follow)
			{
				Debug.Log("Start Following");
				state = BossState.Follow;
				agent.isStopped = false;
			}
		}

		private void OnDrawGizmos()
		{
#if UNITY_EDITOR
			Handles.color = Color.red;
			Handles.DrawWireDisc(transform.position, Vector3.up, attackDistance);
#endif
		}
	}

	public enum BossState { Idle, Follow, Attack, Charge, Prone }
}
