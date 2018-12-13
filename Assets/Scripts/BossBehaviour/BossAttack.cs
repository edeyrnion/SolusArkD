using UnityEngine;

namespace David
{
	public class BossAttack : MonoBehaviour
	{
		BossManager manager;
		BossController controller;

		float timer;


		private void Start()
		{
			manager = GetComponent<BossManager>();
			controller = GetComponent<BossController>();
			timer = manager.AttackTimer;
		}

		private void Update()
		{
			if (manager.state != BossState.Attack) { return; }
			manager.CheckDistanceToTarget();
			controller.Move(0f);
			controller.Attack();
		}
	}
}