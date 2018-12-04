using UnityEngine;

namespace David
{
	public class TriggerZone : MonoBehaviour
	{
		[SerializeField] GameEvent triggerEvent;


		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				triggerEvent.Raise();
			}
		}
	}
}
