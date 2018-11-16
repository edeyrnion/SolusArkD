using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class EnemyNavPoints : MonoBehaviour
	{
		[SerializeField] float wireSphereRadius;
		[SerializeField] float offset;
		[SerializeField] bool visualize;

		public List<GameObject> NavPoints = new List<GameObject>();


		void Awake()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				NavPoints.Add(transform.GetChild(i).gameObject);
			}
			visualize = true;
		}

		void OnDrawGizmos()
		{
			if (visualize)
			{
				Gizmos.color = Color.green;
				for (int i = 0; i < NavPoints.Count; i++)
				{
					Vector3 startPoint = NavPoints[i].transform.position + Vector3.up * offset;
					Vector3 endPoint;
					if (NavPoints[i] == NavPoints[NavPoints.Count - 1])
					{
						endPoint = NavPoints[0].transform.position + Vector3.up * offset;
					}
					else
					{
						endPoint = NavPoints[i + 1].transform.position + Vector3.up * offset;
					}
					Gizmos.DrawLine(startPoint, endPoint);
					Gizmos.DrawWireSphere(startPoint, wireSphereRadius);
				}
			}
		}
	}
}
