using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class GhostBehaviour : MonoBehaviour
	{
		[SerializeField] Transform endPos;
		[SerializeField] Transform center;
		[SerializeField] GameObject moveZone;

		Vector3 ghostTarget;

		float speed = 1f;
		float timer;

		bool behave;
		bool goBack;
		bool moveAround;

		public void StartBehaviour()
		{
			behave = true;
		}

		private void Update()
		{
			if (behave && !moveAround)
			{
				float distance = (transform.localPosition - endPos.localPosition).sqrMagnitude;
				if (distance <= (0.2f * 0.2f))
				{
					moveAround = true;
					ghostTarget = GetNextTarget();
					return;
				}
				transform.Translate(endPos.localPosition * Time.deltaTime * speed);
			}

			if (moveAround)
			{
				MoveAround();
			}

		}

		private void MoveAround()
		{
			if (!goBack)
			{
				float distance = (transform.position - ghostTarget).sqrMagnitude;
				if (distance <= (0.2f * 0.2f))
				{
					ghostTarget = GetNextTarget();
				}
				transform.LookAt(ghostTarget);
				transform.Translate(ghostTarget * Time.deltaTime * speed);
			}
			else if (goBack)
			{
				transform.Translate(center.position * Time.deltaTime * speed);
				transform.LookAt(center);
				timer += Time.deltaTime;
				if (timer >= 2f)
				{
					goBack = false;
				}
			}
		}

		private Vector3 GetNextTarget()
		{
			float x = Random.Range(-20, 20);
			float z = Random.Range(-20, 20);
			return new Vector3(x, 0f, z);
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject == moveZone)
			{
				goBack = true;
			}
		}
	}
}
