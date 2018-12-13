using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
	[SerializeField] David.BossWeaponTrigger trigger;
	public UnityEvent SwordHit;
	Animator anim;

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

			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree"))
			{
				if (attackTimer >= 1f)
				{
					var atk = Random.Range(0, 4);
					switch (atk)
					{
						case 0:
							anim.SetTrigger("Attack1");
							break;
						case 1:
							anim.SetTrigger("Attack2");
							break;
						case 2:
							anim.SetTrigger("Attack3");
							break;
						case 3:
							anim.SetTrigger("Attack4");
							break;
						default:
							break;
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
		SwordHit.Invoke();
		wait = false;
	}

	public void Battlecry()
	{
		anim.SetTrigger("Battlecry");
	}

	public void Charge(float speed)
	{
		anim.SetTrigger("Charge");
	}

	public void Stunned()
	{
		anim.SetTrigger("Stunn");
	}

	public void StandUp()
	{
		anim.SetTrigger("Up");
	}

	public void Death()
	{
		anim.SetTrigger("Death");
	}

	public void Move(float speed)
	{
		anim.SetFloat("Speed", speed);
	}

	public void BackToMove()
	{
		anim.SetTrigger("back");
	}
}
