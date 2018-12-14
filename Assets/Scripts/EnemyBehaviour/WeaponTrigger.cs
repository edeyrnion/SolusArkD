using UnityEngine;
using UnityEngine.Events;

namespace David
{
	public class WeaponTrigger : MonoBehaviour
	{
		[SerializeField] EnemyManager manager;
		public UnityEvent SwordHit;


		private void Start()
		{
			GetComponent<Collider>().enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				SwordHit.Invoke();
				manager.DealDamage(manager.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
