using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class EnemyAI : MonoBehaviour
	{
		[SerializeField] EnemyNavPoints navigation;
		DetectPlayer detectPlayer;
		Attack attack;
		Move move;
		Search search;
		Wait wait;
		LookAtNewTarget look;
		public bool alerted;
		public NavMeshAgent agent;
		public List<GameObject> navPoints;
		public Vector3 nextNavPoint;
		public bool lookingAtTarget;
		public bool waiting;
		public bool attacking;
		public bool stop;


		void Start()
		{
			detectPlayer = GetComponent<DetectPlayer>();
			attack = GetComponent<Attack>();
			move = GetComponent<Move>();
			wait = GetComponent<Wait>();
			search = GetComponent<Search>();
			look = GetComponent<LookAtNewTarget>();
			agent = GetComponent<NavMeshAgent>();
			navPoints = navigation.NavPoints;
			nextNavPoint = navPoints[0].transform.position;
		}

		void Update()
		{
			detectPlayer.Execute();
			if (attacking) { attack.Execute(); }
			if (alerted && !attacking) { search.Execute(); }
			if (waiting) { wait.Execute(); }
			else if (!stop)
			{
				if (!lookingAtTarget)
				{
					NavMeshPath path = new NavMeshPath();
					agent.CalculatePath(nextNavPoint, path);
					look.Execute(path.corners[1]); 					
				}
				else { move.Execute(nextNavPoint); }
			}
		}
	}
}
