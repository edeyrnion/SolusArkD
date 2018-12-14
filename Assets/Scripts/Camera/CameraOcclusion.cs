using System.Collections.Generic;
using UnityEngine;

namespace Matthias
{
    public class CameraOcclusion
    {
        private Camera camera;
        private float ncpDistance, ncpHalfHight, ncpHalfWide;
        private float ncpHalfDiagonal;
        private Vector3 halfExtends;

        private readonly float distanceAdjustment = 0.001f;

        public CameraOcclusion(Camera camera)
        {
            this.camera = camera;
            UpdateCamera();
        }

        public void UpdateCamera()
        {
            ncpDistance = camera.nearClipPlane;
            ncpHalfHight = Mathf.Tan((camera.fieldOfView / 2) * Mathf.Deg2Rad) * camera.nearClipPlane;
            ncpHalfWide = ncpHalfHight * camera.aspect;
            ncpHalfDiagonal = Mathf.Sqrt(Mathf.Pow(ncpHalfHight, 2f) + Mathf.Pow(ncpHalfWide, 2f));
            halfExtends = new Vector3(ncpHalfWide, ncpHalfHight, 0f);
        }

        public bool ClipPlaneCast(Vector3 from, Quaternion orientation, float maxDistance, out float hitDistance, int layerMask = ~0)
        {
            var direction = orientation * -Vector3.forward;

            RaycastHit hitinfo;
            if (Physics.BoxCast(from, halfExtends, direction, out hitinfo, orientation, maxDistance - ncpDistance, layerMask))
            {
                hitDistance = hitinfo.distance + ncpDistance;
                return true;
            }

            hitDistance = maxDistance;
            return false;
        }

        public List<Renderer> ClipPlaneCastAll(Vector3 from, Quaternion orientation, float maxDistance, int layerMask = ~0)
        {
            var direction = orientation * -Vector3.forward;

            RaycastHit[] hitinfos = Physics.BoxCastAll(from, halfExtends, direction, orientation, maxDistance - ncpDistance, layerMask);

            List<Renderer> renderers = new List<Renderer>();

            for (int i = 0; i < hitinfos.Length; i++)
            {
                var a = hitinfos[i].collider.GetComponentsInChildren<Renderer>();
                if (a.Length > 0)
                {
                    renderers.AddRange(a);
                }
            }

            return renderers;
        }

        public bool IsNearCollision(Vector3 from, out Vector3 offset, int layerMask = ~0)
        {
            offset = Vector3.zero;
            bool isNearCollision = false;
            float distance = ncpHalfDiagonal + distanceAdjustment;

            for (int i = 0; i < 360; i = i + 45)
            {
                var direction = Quaternion.Euler(0f, i, 0f) * Vector3.forward;
                Ray ray = new Ray(from, direction);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, distance, layerMask))
                {
                    offset += direction.normalized * (hit.distance - distance);
                    isNearCollision = true;

                    // DEBUG:
                    Debug.DrawLine(from, from + direction * distance, Color.red);
                }
            }

            return isNearCollision;
        }
    }
}
