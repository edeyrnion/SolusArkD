using UnityEngine;

namespace Matthias
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 targetOffset = new Vector3(0f, 0f, 0f);
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private LayerMask fadeOutLayer;

        // Camera distance.
        [Header("Zoom")]
        [SerializeField] [Range(0f, 20f)] private float distance = 12f;

        // Camera rotation limits.
        [Header("Clap Rotation")]
        [SerializeField] [Range(-90f, 0f)] private float minAngleX = -45.0f;
        [SerializeField] [Range(0f, 90f)] private float maxAngleX = 65.0f;

        // Camera controll sensitivities.
        [Header("Sensivity")]
        [SerializeField] [Range(1f, 10f)] private float rotationSensivityX = 6.0f;
        [SerializeField] [Range(1f, 10f)] private float rotationSensivityY = 4.0f;

        // Camera movement smoothness.
        [Header("Smoothing")]
        [SerializeField] [Range(0f, 2f)] private float movementSmooth = 0.3f;
        [SerializeField] [Range(0f, 2f)] private float rotationSmooth = 0.3f;
        [SerializeField] [Range(0f, 2f)] private float zoomSmooth = 1f;

        // Camera desired values.
        private float desiredRotationX;
        private float desiredRotationY;

        // Camera velocities references.
        private Vector3 movementVel;
        private float rotationVelY;
        private float rotationVelX;
        private float zoomVel;
        private float adjustVel;

        // Default values.
        private Transform defaultTarget;
        private Vector3 defaultTargetOffset;
        private float defaultDistance;

        // Other.
        private float rotationY;
        private float rotationX;
        private float adjustedDistance;

        private struct Pivot
        {
            public Vector3 Position { get; set; }
            public Quaternion Rotation { get; set; }
        }
        private Pivot pivot;

        // Camera occlusion.
        private CameraOcclusion cameraOcclusion;
        private ObstacleFade obstacleFade;

        private void Start()
        {
            if (target == null)
            {
                if (GameObject.FindGameObjectWithTag("Player").transform != null)
                {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
                }
                else
                {
                    Debug.LogError("[" + gameObject.name + "] [" + GetType().Name + "] No target assigned and no player found.");
                }
            }

            // Backup default values.
            defaultTarget = target;
            defaultTargetOffset = targetOffset;
            defaultDistance = distance;

            // Set pivot position and rotation.
            var position = target.position + target.TransformDirection(targetOffset);
            var rotationY = desiredRotationY = target.rotation.eulerAngles.y;
            pivot = new Pivot() { Position = position, Rotation = Quaternion.Euler(0f, rotationY, 0f) };

            //Set camera distance.
            adjustedDistance = distance;

            cameraOcclusion = new CameraOcclusion(GetComponent<Camera>());
            obstacleFade = new ObstacleFade();

            // TODO: Should not be handled here.
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            // Compute input.
            ControllRotation(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Update pivot position.
            var targetPosition = target.position + target.TransformDirection(targetOffset);
            pivot.Position = Vector3.SmoothDamp(pivot.Position, targetPosition, ref movementVel, movementSmooth);

            // Update pivot rotation.
            rotationY = Mathf.SmoothDamp(rotationY, desiredRotationY, ref rotationVelY, rotationSmooth * 0.1f);
            rotationX = Mathf.SmoothDamp(rotationX, desiredRotationX, ref rotationVelX, rotationSmooth * 0.1f);
            if (rotationY > 360)
            {
                desiredRotationY -= 360;
                rotationY -= 360;
            }
            else if (rotationY < 0)
            {
                desiredRotationY += 360;
                rotationY += 360;
            }
            pivot.Rotation = Quaternion.Euler(rotationX, rotationY, 0f);

            // Update camera distance.
            float collisionDistance;
            if (cameraOcclusion.ClipPlaneCast(pivot.Position, pivot.Rotation, distance, out collisionDistance, collisionLayer))
            {
                zoomVel = 0f;
                if (collisionDistance > adjustedDistance)
                {
                    adjustedDistance = Mathf.SmoothDamp(adjustedDistance, collisionDistance, ref adjustVel, zoomSmooth * 0.1f);
                }
                else
                {
                    adjustVel = 0f;
                    adjustedDistance = collisionDistance;
                }
            }
            else
            {
                adjustVel = 0f;
                adjustedDistance = Mathf.SmoothDamp(adjustedDistance, distance, ref zoomVel, zoomSmooth * 0.1f);
            }

            // Update camera postion.
            transform.position = pivot.Position + (pivot.Rotation * (-Vector3.forward * adjustedDistance));

            // Update camera rotation.
            transform.rotation = pivot.Rotation;

            // Fade out obstacles.
            var renderers = cameraOcclusion.ClipPlaneCastAll(pivot.Position, pivot.Rotation, distance, fadeOutLayer);
            obstacleFade.Fade(renderers);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Objects fading out: " + obstacleFade.FadeOut.Count.ToString());
            GUI.Label(new Rect(10, 30, 200, 20), "Objects fading in: " + obstacleFade.FadeIn.Count.ToString());
            GUI.Label(new Rect(10, 50, 200, 20), "Collision Distance: " + adjustedDistance.ToString());
        }

        private void ControllRotation(float axisX, float axisY)
        {
            desiredRotationY += axisX * rotationSensivityX;
            desiredRotationX += axisY * -1 * rotationSensivityY;
            desiredRotationX = Mathf.Clamp(desiredRotationX, minAngleX, maxAngleX);
        }

        // Public access.
        public void SetTarget(Transform newTarget, bool useLerp = true)
        {
            if (useLerp)
            {
                target = newTarget;
            }
            else
            {
                target = newTarget;
                pivot.Position = newTarget.position + newTarget.TransformDirection(targetOffset);
                rotationY = desiredRotationY = newTarget.rotation.eulerAngles.y;
                rotationX = desiredRotationX = 0f;
            }
        }

        public void RestTarget(bool useLerp = true)
        {
            if (useLerp)
            {
                target = defaultTarget;
            }
            else
            {
                target = defaultTarget;
                pivot.Position = target.position + target.TransformDirection(targetOffset);
                rotationY = desiredRotationY = target.rotation.eulerAngles.y;
                rotationX = desiredRotationX = 0f;
            }
        }

        public void SetTargetOffset(Vector3 offset, bool useLerp = true)
        {
            if (useLerp)
            {
                targetOffset = offset;
            }
            else
            {
                targetOffset = offset;
                pivot.Position = target.position + target.TransformDirection(targetOffset);
            }
        }

        public void RestTargetOffset(bool useLerp = true)
        {
            if (useLerp)
            {
                targetOffset = defaultTargetOffset;
            }
            else
            {
                targetOffset = defaultTargetOffset;
                pivot.Position = target.position + target.TransformDirection(targetOffset);
            }
        }

        public void SetCameraDistance(float distance)
        {
            this.distance = distance;
        }

        public void RestCameraDistance()
        {
            distance = defaultDistance;
        }
    }
}
