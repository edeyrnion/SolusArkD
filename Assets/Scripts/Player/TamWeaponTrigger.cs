using UnityEngine;

namespace David
{
	public class TamWeaponTrigger : MonoBehaviour
	{
		[SerializeField] PlayerManager manager;


		private void Start()
		{
			GetComponent<Collider>().enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Enemy"))
			{
				manager.DoDamage(other.gameObject, manager.Stats.Damage);
				GetComponent<Collider>().enabled = false;
			}
			if (other.gameObject.CompareTag("Ghost"))
			{
				manager.AttackGhost(other.gameObject, manager.Stats.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
