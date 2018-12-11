using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "GhostStats", menuName = "ScriptableObjects/GhostStats", order = 5)]
	public class GhostStats : ScriptableObject
	{
		public int Health;	
	}
}