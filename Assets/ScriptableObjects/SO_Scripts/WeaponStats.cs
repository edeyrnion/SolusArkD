using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapons", order = 2)]
	public class WeaponStats : ScriptableObject
	{
		public int Damage;
		public float AttackWaitTime;
		public WeaponType weaponType;
	}

	public enum WeaponType { Sword, Axe, Hammer };

}