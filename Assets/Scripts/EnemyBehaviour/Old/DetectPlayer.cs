using UnityEngine;
using UnityEditor;

namespace David
{
	public class DetectPlayer : MonoBehaviour
	{
		[SerializeField] GameObject player;
		[SerializeField] float detectionRadius;
		[SerializeField] bool visualize = true;
		public GameObject Target;
		public float alertRadius;
		public bool spottedPlayer;
		EnemyAI enemyAI;
		Color color;
		

		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
		}

		public void Execute()
		{
			Vector3 myPos = transform.position;
			Vector3 playerPos = player.transform.position;
			float distanceToPlayer = (myPos - playerPos).magnitude;
			if (distanceToPlayer > alertRadius)
			{
				Target = null;
				color = Color.green;
				return;
			}
			Ray ray = new Ray(myPos, playerPos - myPos);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, alertRadius))
			{
				Target = hit.transform.gameObject;
				if (Target.CompareTag("Player"))
				{
					color = Color.yellow;
					enemyAI.alerted = true;
					if (Physics.Raycast(ray, out hit, detectionRadius))
					{
						color = Color.red;
						spottedPlayer = true;
					}
				}
				else { color = Color.green; }
			}
		}

		private void OnDrawGizmos()
		{
			if (visualize)
			{
				Gizmos.color = color;
				Gizmos.DrawLine(transform.position, player.transform.position);
				Handles.color = Color.yellow;
				Handles.DrawWireDisc(transform.position, Vector3.up, alertRadius);
				Handles.color = Color.red;
				Handles.DrawWireDisc(transform.position, Vector3.up, detectionRadius);
			}
		}
	}
}
