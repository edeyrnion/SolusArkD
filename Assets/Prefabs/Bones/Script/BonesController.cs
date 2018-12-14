using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class BonesController : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        anim.SetFloat("Speed", speed);
    }
}
