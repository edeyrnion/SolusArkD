using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private Vector3 cameraForward;
    private Vector3 moveVector;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (cam == null)
        {
            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning("[" + gameObject.name + "] [" + GetType().Name + "] No camera assigned and no main camera found. Camera relative controlls switched off.");
            }
        }
    }

    private void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        if (cam != null)
        {
            cameraForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            moveVector = moveV * cameraForward + moveH * cam.right;
        }
        else
        {
            moveVector = moveV * Vector3.forward + moveH * Vector3.right;
        }

        rb.velocity = moveVector * 10f;
        if (moveVector != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(moveVector, Vector3.up);
        }
    }
}
