﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class BossProne : MonoBehaviour
	{
		[SerializeField] float proneTimer;

		BossManager manager;

		float time;


		private void Start()
		{
			manager = GetComponent<BossManager>();
		}

		private void Update()
		{
			if (manager.state != BossState.Prone) { return; }
			time += Time.deltaTime;
			if (time >= proneTimer)
			{
				Debug.Log("Not prone anymore!");
				time = 0f;
				manager.Break = false;
				manager.Charging = false;
				manager.agent.isStopped = false;
				manager.state = BossState.Follow;
			}
		}
	}
}
