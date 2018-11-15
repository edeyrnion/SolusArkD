using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavPoints : MonoBehaviour
{
	public List<GameObject> navPoints = new List<GameObject>();
	[SerializeField] float wireSphereRadius;
	[SerializeField] float offset;
	[SerializeField] bool visualize;


	void Awake()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			navPoints.Add(transform.GetChild(i).gameObject);
		}
	}	

	private void OnDrawGizmos()
	{
		if (visualize)
		{
			Gizmos.color = Color.green;
			for (int i = 0; i < navPoints.Count; i++)
			{
				Vector3 startPoint = navPoints[i].transform.position + Vector3.up * offset;
				Vector3 endPoint;
				if (navPoints[i] == navPoints[navPoints.Count - 1])
				{
					endPoint = navPoints[0].transform.position + Vector3.up * offset;
				}
				else
				{
					endPoint = navPoints[i + 1].transform.position + Vector3.up * offset;
				}
				Gizmos.DrawLine(startPoint, endPoint);
				Gizmos.DrawWireSphere(startPoint, wireSphereRadius);
			}			
		}
	}
}
