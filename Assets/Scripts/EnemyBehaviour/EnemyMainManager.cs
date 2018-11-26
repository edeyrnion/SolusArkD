using UnityEngine;
using System.Collections.Generic;

namespace David
{
	public class EnemyMainManager : MonoBehaviour
	{
		[SerializeField] GameObject[] enemies;
		public List<bool> Alerted;


		private void Update()
		{
			for (int i = 0; i < enemies.Length; i++)
			{
				EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
				if (manager.CurrentState == State.Following || manager.CurrentState == State.Attacking)
				{
					Alerted[i] = true;
				}
				else { Alerted[i] = false; }
			}

			if (Alerted.Contains(true))
			{
				for (int i = 0; i < enemies.Length; i++)
				{
					EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = true;
				}
			}
			else
			{
				for (int i = 0; i < enemies.Length; i++)
				{
					EnemyManager manager = enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = false;
				}
			}
		}
	}
}
