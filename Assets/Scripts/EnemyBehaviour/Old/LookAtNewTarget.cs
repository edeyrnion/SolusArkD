using UnityEngine;

namespace David
{
	public class LookAtNewTarget : MonoBehaviour
	{
		public float TurnSpeed;
		EnemyAI enemyAI;	


		private void Start()
		{
			enemyAI = GetComponent<EnemyAI>();
		}

		public void Execute(Vector3 lookTarget)
		{				
			Vector3 targetDir = lookTarget - transform.position;
			Debug.DrawRay(transform.position, targetDir, Color.red);
			float step = TurnSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
			if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetDir)) <= 0.1f) { enemyAI.lookingAtTarget = true; }
		}
	}
}
