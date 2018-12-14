using UnityEngine;
using UnityEngine.Events;

namespace David
{
	public class TamWeaponTrigger : MonoBehaviour
	{
		[SerializeField] PlayerManager manager;
		public UnityEvent SwordHit;


		private void Start()
		{
			GetComponent<Collider>().enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Enemy"))
			{
				SwordHit.Invoke();
				manager.DoDamage(other.gameObject, manager.Stats.Damage);
				GetComponent<Collider>().enabled = false;
			}
			if (other.gameObject.CompareTag("Ghost"))
			{
				SwordHit.Invoke();
				manager.AttackGhost(other.gameObject, manager.Stats.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
