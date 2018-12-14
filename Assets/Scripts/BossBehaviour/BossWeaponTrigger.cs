using UnityEngine;
using UnityEngine.Events;

namespace David
{
	public class BossWeaponTrigger : MonoBehaviour
	{
		[SerializeField] BossManager manager;
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
				manager.DoDamage(manager.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
