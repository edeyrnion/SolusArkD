using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class GhostBehaviour : MonoBehaviour
	{
		[SerializeField] Transform endPos;
		[SerializeField] Transform center;
		[SerializeField] Transform playerPos;

		Vector3 ghostTarget;

		float speed = 1f;
		float timer;

		bool behave;
		bool moveAround;

		public void StartBehaviour()
		{
			behave = true;
		}

		public void StopBehaviour()
		{
			behave = false;
			moveAround = false;
			transform.localPosition = Vector3.zero;
			transform.GetChild(0).localRotation = Quaternion.Euler(Vector3.zero);
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
			float distance = (transform.position - ghostTarget).sqrMagnitude;
			if (distance <= (0.2f * 0.2f))
			{
				ghostTarget = GetNextTarget();
			}
			transform.position = Vector3.MoveTowards(transform.position, ghostTarget, Time.deltaTime * speed * 5);
			Vector3 newDir = playerPos.position;
			newDir.y = transform.GetChild(0).transform.position.y;
			transform.GetChild(0).transform.LookAt(newDir, Vector3.up);
		}

		private Vector3 GetNextTarget()
		{
			float x = Random.Range(-40, 50);
			float z = Random.Range(-35, 30);
			return new Vector3(center.transform.position.x + x, transform.position.y, center.transform.position.z + z);
		}
	}
}
