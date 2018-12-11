using UnityEngine;

namespace David
{
	public class EndScreenRef : MonoBehaviour
	{
		[SerializeField] PlayerStats playerStats;
		[SerializeField] GameObject endScreen;


		private void Start()
		{
			playerStats.EndScreen = endScreen;
		}		
	}
}
