using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
	public class PlayerStats : ScriptableObject
	{
		[SerializeField] WeaponStats weapon;
		public int Health;
		public float WalkingSpeed;
	} 
}