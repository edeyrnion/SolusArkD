using UnityEngine;

namespace David
{
	public class WeaponTrigger : MonoBehaviour
	{
		[SerializeField] EnemyManager manager;
		[SerializeField] BanditController banditController;


		private void OnTriggerEnter(Collider other)
		{			
			if (other.gameObject.CompareTag("Player") && banditController.Attacking)
			{
				manager.DealDamage(manager.Damage);
				banditController.Attacking = false;				
			}
		}
	}
}
