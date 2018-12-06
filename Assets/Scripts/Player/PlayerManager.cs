using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class PlayerManager : MonoBehaviour
	{
		public PlayerStats Stats;

		AttackZone attackZone;

		float timer;


		private void Start()
		{
			attackZone = transform.GetChild(0).GetComponent<AttackZone>();
		}

		private void Update()
		{
			timer += Time.deltaTime;
			if (timer >= Stats.AttackTimer && Input.GetKeyDown(KeyCode.Mouse0)) { CheckIfEnemiesInReach(); }
		}

		public void CheckIfEnemiesInReach()
		{
			timer = 0f;
			var enemies = attackZone.enemies;			
			if (enemies.Count == 0) { return; }

			for (int i = 0; i < enemies.Count; i++)
			{
				Attack(enemies[i], Stats.Damage);
			}
		}

		public void Attack(GameObject target, int damage)
		{
			target.GetComponent<EnemyManager>().Health -= damage;

			if (target.GetComponent<EnemyManager>().Health <= 0)
			{
				attackZone.enemies.Remove(target);
				Destroy(target);
			}
		}
	}
}
