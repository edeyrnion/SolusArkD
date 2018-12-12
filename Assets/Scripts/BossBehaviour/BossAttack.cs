using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class BossAttack : MonoBehaviour
	{
		BossManager manager;

		float timer;


		private void Start()
		{
			manager = GetComponent<BossManager>();
			timer = manager.AttackTimer;
		}

		private void Update()
		{
			if (manager.state != BossState.Attack) { return; }
			manager.CheckDistanceToTarget();
			timer += Time.deltaTime;
			if (timer >= manager.AttackTimer)
			{
				timer = 0f;
				DoDamage(manager.Damage);
			}

		}

		public void DoDamage(int damage)
		{
			manager.target.GetComponent<PlayerManager>().Stats.Health -= damage;
			manager.target.GetComponent<PlayerManager>().UpdateHealthBar();
		}
	}
}