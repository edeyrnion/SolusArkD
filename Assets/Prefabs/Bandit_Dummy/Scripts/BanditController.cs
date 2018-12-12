using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class BanditController : MonoBehaviour
{
	public bool Attacking;

	Animator anim;
	Rigidbody rb;

	float lastTime;
	float attackTimer;	


	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		var move = Input.GetAxis("Vertical");

		Move(move);
	}

	public void Attack()
	{
		attackTimer += Time.deltaTime;

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
		{			
			if (attackTimer >= 1f)
			{
				if (Random.Range(0, 2) > 0)
				{
					anim.SetTrigger("Attack1");
					Attacking = true;
				}
				else
				{
					anim.SetTrigger("Attack2");
					Attacking = true;
				}				
				attackTimer = 0f;
			}
		}
	}

	public void Death()
	{
		anim.SetTrigger("Death");
	}

	public void Move(float speed)
	{
		anim.SetFloat("Speed", speed);
	}
}
