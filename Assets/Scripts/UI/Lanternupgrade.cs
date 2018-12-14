using UnityEngine;


public class Lanternupgrade : MonoBehaviour
{
	[SerializeField] GameObject panel;
	[SerializeField] David.Lantern lantern;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			panel.SetActive(true);
			lantern.MaxLanternUses++;
			Destroy(this);
		}
	}
}
