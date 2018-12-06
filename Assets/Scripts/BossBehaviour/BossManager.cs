using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class BossManager : GameEventListener
	{		
		[SerializeField] float chargeTimer;

		public BossState state = BossState.Idle;

		public BossAttack BossAttack;
		public GhostBehaviour ghostBehaviour;
		public NavMeshAgent agent;
		public GameObject target;

		public int Damage;
		public int ChargeDamage;

		public float AttackTimer;
		public float attackDistance;
		public float bossSpeed;

		public bool Charging = true;
		public bool Break;
		public bool HitPlayer;


		float time;


		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			agent.speed = bossSpeed;
			BossAttack = GetComponent<BossAttack>();
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

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Obstacle") && state == BossState.Charge)
			{
				Break = true;
				ghostBehaviour.gameObject.SetActive(true);
				ghostBehaviour.StartBehaviour();
			}
			else if (other.gameObject.CompareTag("Player") && state == BossState.Charge)
			{
				HitPlayer = true;
				print("I need to do something here ._. (Charged into Player)");
				//bool = active (wait for a short time and then start moving/attacking)
				//Do lot's of damage and knock player prone
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
