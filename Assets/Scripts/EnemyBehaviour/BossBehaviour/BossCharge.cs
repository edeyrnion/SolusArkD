using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace David
{
	public class BossCharge : MonoBehaviour
	{
		[SerializeField] float waitTime;
		[SerializeField] float chargeSpeed;

		BossManager manager;

		Vector3 chargeTarget;

		float time;
		float chargeTime;
		bool charging;


		private void Start()
		{
			manager = GetComponent<BossManager>();
		}

		private void Update()
		{
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
				float chargeTimer = 1.5f;
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
