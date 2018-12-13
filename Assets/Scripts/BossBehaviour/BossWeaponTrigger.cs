using UnityEngine;

namespace David
{
	public class BossWeaponTrigger : MonoBehaviour
	{
		[SerializeField] BossManager manager;
		[SerializeField] BossController bossController;


		private void Start()
		{
			GetComponent<Collider>().enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				manager.DoDamage(manager.Damage);
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
