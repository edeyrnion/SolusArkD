using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
	public class PlayerStats : ScriptableObject
	{
		[SerializeField] WeaponStats weapon;	
		public GameObject EndScreen;
		public int Health;
		public int Damage;
		public float AttackTimer;
		public float WalkingSpeed;		
	}
}