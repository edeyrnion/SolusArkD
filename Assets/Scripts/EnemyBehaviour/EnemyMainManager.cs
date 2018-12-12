using UnityEngine;
using System.Collections.Generic;

namespace David
{
	public class EnemyMainManager : MonoBehaviour
	{
		public List<GameObject> Enemies;
		public List<bool> Alerted;


		private void Update()
		{
			for (int i = 0; i < Enemies.Count; i++)
			{
				if (Enemies[i] == null)
				{
					Enemies.Remove(Enemies[i]);
					continue;
				}
				EnemyManager manager = Enemies[i].GetComponent<EnemyManager>();
				if (manager.CurrentState == State.Following || manager.CurrentState == State.Attacking)
				{
					Alerted[i] = true;
				}
				else { Alerted[i] = false; }
			}

			if (Alerted.Contains(true))
			{
				for (int i = 0; i < Enemies.Count; i++)
				{
					EnemyManager manager = Enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = true;
				}
			}
			else
			{
				for (int i = 0; i < Enemies.Count; i++)
				{
					EnemyManager manager = Enemies[i].GetComponent<EnemyManager>();
					manager.OtherEnemyIsAlerted = false;
				}
			}
		}
	}
}
