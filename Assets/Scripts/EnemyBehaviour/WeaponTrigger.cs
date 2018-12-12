using UnityEngine;

namespace David
{
	public class WeaponTrigger : MonoBehaviour
	{
		[SerializeField] EnemyManager manager;
		[SerializeField] BanditController banditController;


		private void Start()
		{
			GetComponent<Collider>().enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				manager.DealDamage(manager.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
