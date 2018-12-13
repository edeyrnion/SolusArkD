using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace David
{
	public class CloseBossRoom : GameEventListener
	{
		[SerializeField] float endPos;
		[SerializeField] float closeSpeed;
		float timer;
		bool closing;

		public void CloseGate()
		{
			closing = true;
		}

		private void Update()
		{
			if (closing)
			{
				float y = Time.deltaTime * closeSpeed;
				transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
				if (transform.position.y >= endPos)
				{
					closing = false;
					print("test");
					Destroy(this);
				}
			}
		}
	}
}
