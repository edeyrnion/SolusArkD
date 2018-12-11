using UnityEngine;
using TMPro;

namespace David
{
	public class PlayerManager : MonoBehaviour
	{
		[SerializeField] Lantern lantern;

		public PlayerStats Stats;

		AttackZone attackZone;
		GameObject endScreen;
		TextMeshProUGUI text;

		float timer;

		bool endScreenActive;


		private void Start()
		{
			Stats.Health = 30;
			endScreenActive = false;
			endScreen = Stats.EndScreen;
			text = endScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			attackZone = transform.GetChild(0).GetComponent<AttackZone>();
		}

		private void Update()
		{
			timer += Time.deltaTime;
			if (timer >= Stats.AttackTimer && Input.GetKeyDown(KeyCode.Mouse0)) { CheckIfEnemiesInReach(); }
			if (Stats.Health <= 0) { OpenEndScreen("Game Over!"); }
		}

		public void CheckIfEnemiesInReach()
		{
			timer = 0f;
			var enemies = attackZone.enemies;
			if (enemies.Count == 0) { return; }

			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i].name == "Ghost")
				{
					if (!lantern.isActive) { continue; }
					AttackGhost(enemies[i], Stats.Damage);
					continue;
				}
				Attack(enemies[i], Stats.Damage);
			}
		}

		public void Attack(GameObject target, int damage)
		{
			target.GetComponent<EnemyManager>().Health -= damage;

			if (target.GetComponent<EnemyManager>().Health <= 0)
			{
				target.GetComponent<BanditController>().Death();
				attackZone.enemies.Remove(target);
				target.GetComponent<EnemyManager>().ChangeState(State.Dead);
			}
		}

		void AttackGhost(GameObject target, int damage)
		{
			target.GetComponent<GhostBehaviour>().Health -= damage;

			if (target.GetComponent<GhostBehaviour>().Health <= 0)
			{
				OpenEndScreen("We did it!");
			}
		}

		void OpenEndScreen(string screenText)
		{
			Time.timeScale = 0f;
			if (endScreenActive) { return; }
			endScreenActive = true;
			text.text = screenText;
			endScreen.SetActive(true);
			Cursor.visible = true;
		}
	}
}
