using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class AttackZone : MonoBehaviour
	{
		public List<GameObject> enemies = new List<GameObject>();


		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Enemy"))
			{
				enemies.Add(other.gameObject);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.CompareTag("Enemy"))
			{
				enemies.Remove(other.gameObject);
			}
		}
	} 
}
