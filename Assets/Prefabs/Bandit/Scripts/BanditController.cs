using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(Rigidbody))]
public class BanditController : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    float lastTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        var move = Input.GetAxis("Vertical");

        Move(move);
    }

    public void Attack()
    {
        var currentTime = Time.time;
        if (lastTime < currentTime - 1f)
        {
            lastTime = currentTime;
            if (Random.Range(0, 2) > 0)
            {
                anim.SetTrigger("Attack1");
            }
            else
            {
                anim.SetTrigger("Attack2");
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
