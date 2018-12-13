using UnityEngine;
using Matthias;

[RequireComponent(typeof(TamCharacterController))]
public class TamCharacterInput : MonoBehaviour
{
    private TamCharacterController character;
    private Transform cam;
    private Vector3 camForward;
    private Vector3 move;
    private bool jump;
    private bool atk;
    private float lastTime;

    private void Start()
    {
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        }

        character = GetComponent<TamCharacterController>();
    }

    private void Update()
    {
        if (!jump)
        {
            jump = CInput.GetButtonDown(CButton.Jump);
        }

        if (!atk)
        {
            atk = CInput.GetButtonDown(CButton.Attack);
        }
    }

    private void FixedUpdate()
    {
        float h = CInput.GetAxis(CAxis.MoveHorizontal);
        float v = CInput.GetAxis(CAxis.MoveVertical);
        bool crouch = CInput.GetButton(CButton.Crouch);

        var currentTime = Time.time;
        if (lastTime < currentTime - 0.6f)
        {
            lastTime = currentTime;
            if (atk)
            {
                Debug.Log("Attack");
                character.Attack();
                atk = false;
            }
        }

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = v * camForward + h * cam.right;
        }
        else
        {
            move = v * Vector3.forward + h * Vector3.right;
        }
        if (CInput.GetButton(CButton.Walk)) move *= 0.5f;

        character.Move(move, crouch, jump);
        jump = false;
    }
}
