using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class BossAttack : MonoBehaviour
	{
		BossManager manager;


		private void Start()
		{
			manager = GetComponent<BossManager>();
		}

		private void Update()
		{
			if (manager.state != BossState.Attack) { return; }			
			manager.CheckDistanceToTarget();
		}
	}
}
