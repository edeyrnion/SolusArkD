using UnityEngine;

namespace David
{
	[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 0)]
	public class EnemyStats : ScriptableObject
	{
		public int Health;
		public int Damage;
		public float AttackWaitTime;
		public float AlertRadius;
		public float DetectionRadius;
		public float WalkingSpeed;
	} 
}
