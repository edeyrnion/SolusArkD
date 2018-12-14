using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class BanditController : MonoBehaviour
{
	[SerializeField] David.WeaponTrigger trigger;	
	public UnityEvent DeathSound;

	Animator anim;
	Rigidbody rb;

	float lastTime;
	float attackTimer;
	bool wait;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}	

	public void Attack()
	{
		if (!wait)
		{
			attackTimer += Time.deltaTime;

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
			{
				if (attackTimer >= 1f)
				{
					if (Random.Range(0, 2) > 0)
					{
						anim.SetTrigger("Attack1");
					}
					else
					{
						anim.SetTrigger("Attack2");
					}
					attackTimer = 0f;
					wait = true;					
					IEnumerator coroutine = Wait(0.5f);
					StartCoroutine(coroutine);
				}
			}
		}
	}

	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		trigger.GetComponent<Collider>().enabled = true;
		wait = false;
	}

	public void Death()
	{
		anim.SetTrigger("Death");
		IEnumerator coroutine = Dead(2f);
		StartCoroutine(coroutine);
	}

	IEnumerator Dead(float time)
	{
		yield return new WaitForSeconds(time);
		DeathSound.Invoke();
        GetComponent<Collider>().enabled = false;
    }

	public void Move(float speed)
	{
		anim.SetFloat("Speed", speed);
	}
}
