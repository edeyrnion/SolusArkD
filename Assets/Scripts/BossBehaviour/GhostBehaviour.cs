using UnityEngine;

namespace David
{
	public class GhostBehaviour : MonoBehaviour
	{
		[SerializeField] GhostStats stats;
		[SerializeField] Transform endPos;
		[SerializeField] Transform center;
		[SerializeField] Transform playerPos;
		[SerializeField] float speed = 1f;

		public int Health;	

		Vector3 ghostTarget;

		float timer;

		bool behave;
		bool moveAround;


		private void Start()
		{
			Health = stats.Health;
		}

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
			transform.GetChild(0).gameObject.SetActive(false);
			gameObject.SetActive(false);
		}

		private void Update()
		{			
			if (behave && !moveAround)
			{
				float distance = (transform.localPosition - endPos.localPosition).sqrMagnitude;
				if (distance <= 0.05f)
				{
					moveAround = true;
					ghostTarget = GetNextTarget();
					return;
				}
				transform.position = Vector3.MoveTowards(transform.position, endPos.position, Time.deltaTime * speed);
			}

			if (moveAround)
			{
				MoveAround();
			}
		}

		private void MoveAround()
		{
			float distance = (transform.position - ghostTarget).sqrMagnitude;
			if (distance <= 0.05f)
			{
				ghostTarget = GetNextTarget();
			}
			transform.position = Vector3.MoveTowards(transform.position, ghostTarget, Time.deltaTime * speed * 0.75f);
			Vector3 newDir = playerPos.position;
			newDir.y = transform.GetChild(0).transform.position.y;
			transform.GetChild(0).transform.LookAt(newDir, Vector3.up);
		}

		private Vector3 GetNextTarget()
		{
			float x = Random.Range(-13, 13);
			float z = Random.Range(-13, 13);
			return new Vector3(center.transform.position.x + x, transform.position.y, center.transform.position.z + z);
		}
	}
}
