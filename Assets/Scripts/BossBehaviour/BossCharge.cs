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

		Vector3 chargeTarget;

		float time;
		float chargeTime;
		float knockBackTimer;

		bool charging;
		bool knockBack;


		private void Start()
		{
			manager = GetComponent<BossManager>();
		}

		private void Update()
		{
			if (knockBack) { KnockBackPlayer(); }

			if (manager.state != BossState.Charge) { return; }
			if (!charging)
			{
				time += Time.deltaTime;
				if (time >= waitTime)
				{
					time = 0f;
					charging = true;
				}
			}

			if (charging)
			{
				if (manager.Break)
				{
					chargeTime = 0f;
					charging = false;
					manager.state = BossState.Prone;
					Debug.Log("Knocked Prone!");
					return;
				}

				if (manager.HitPlayer)
				{
					manager.HitPlayer = false;
					chargeTime = 0f;
					charging = false;
					manager.BossAttack.DoDamage(manager.ChargeDamage);
					knockBack = true;
					Debug.Log("Did Charge Damage!");
					manager.state = BossState.Follow;
					return;
				}

				float chargeTimer = 1f;
				if (chargeTime >= chargeTimer) { return; }

				transform.position += transform.forward * Time.deltaTime * chargeSpeed;

				chargeTime += Time.deltaTime;
				if (chargeTime >= chargeTimer)
				{
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
