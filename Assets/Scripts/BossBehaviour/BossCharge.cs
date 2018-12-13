using System.Collections;
using UnityEngine;

namespace David
{
	public class BossCharge : MonoBehaviour
	{
		[SerializeField] float waitTime;
		[SerializeField] float knockBackTime;
		[SerializeField] float chargeSpeed;
		[SerializeField] float force;

		BossManager manager;
		BossController controller;

		Vector3 chargeTarget;

		float time;
		float chargeTime;
		float knockBackTimer;

		bool charging;
		bool knockBack;


		private void Start()
		{
			manager = GetComponent<BossManager>();
			controller = GetComponent<BossController>();
		}

		private void Update()
		{
			if (knockBack) { KnockBackPlayer(); }

			if (manager.state != BossState.Charge) { return; }
			if (!charging)
			{
				//controller.Battlecry();
				time += Time.deltaTime;
				if (time >= waitTime)
				{
					time = 0f;
					charging = true;
					controller.Charge(0.1f);
				}
			}

			if (charging)
			{
				if (manager.Break)
				{
					chargeTime = 0f;
					charging = false;
					controller.Stunned();
					manager.state = BossState.Prone;
					Debug.Log("Knocked Prone!");
					return;
				}

				if (manager.HitPlayer)
				{
					manager.HitPlayer = false;
					chargeTime = 0f;
					charging = false;
					manager.DoDamage(manager.ChargeDamage);
					knockBack = true;
					Debug.Log("Did Charge Damage!");
					controller.BackToMove();
					controller.Move(0f);
					manager.state = BossState.Follow;
					return;
				}

				float chargeTimer = 0.75f;
				if (chargeTime >= chargeTimer) { return; }

				transform.position += transform.forward * Time.deltaTime * chargeSpeed;

				chargeTime += Time.deltaTime;
				if (chargeTime >= chargeTimer)
				{
					controller.BackToMove();
					controller.Move(0f);
					IEnumerator coroutine = Wait(2f);
					StartCoroutine(coroutine);
				}
			}
		}

		void KnockBackPlayer()
		{
			knockBackTimer += Time.deltaTime;

			Vector3 direction = gameObject.transform.forward;
			manager.target.transform.position += direction * Time.deltaTime * force;

			if (knockBackTimer >= knockBackTime)
			{
				knockBackTimer = 0f;
				knockBack = false;
				manager.Charging = false;
			}
		}

		IEnumerator Wait(float time)
		{
			yield return new WaitForSeconds(time);
			chargeTime = 0f;
			charging = false;
			manager.Charging = false;
			manager.state = BossState.Follow;
			manager.agent.isStopped = false;
		}
	}
}
