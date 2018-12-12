using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    Animator anim;

    float lastTime;

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
        var currentTime = Time.time;
        if (lastTime < currentTime - 1f)
        {
            lastTime = currentTime;
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
        }
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
}
