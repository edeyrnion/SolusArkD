using UnityEngine;
using System.Collections.Generic;

namespace David
{
	public class EnemyMainManager : MonoBehaviour
	{
		[SerializeField] List<GameObject> enemies;
		public List<bool> Alerted;


		private void Update()
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i] == null)
				{
					enemies.Remove(enemies[i]);
					continue;
				}
				EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
				if (manager.CurrentState == State.Following || manager.CurrentState == State.Attacking)
				{
					Alerted[i] = true;
				}
				else { Alerted[i] = false; }
			}

			if (Alerted.Contains(true))
			{
				for (int i = 0; i < enemies.Count; i++)
				{
					EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = true;
				}
			}
			else
			{
				for (int i = 0; i < enemies.Count; i++)
				{
					EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = false;
				}
			}
		}
	}
}
