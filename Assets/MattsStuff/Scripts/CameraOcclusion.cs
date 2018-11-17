using UnityEngine;

public class CameraOcclusion
{
    private Camera camera;
    private float ncpDistance, ncpHalfHight, ncpHalfWide, ncpHalfDiagonal;
    private Vector3 halfExtends;

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

    public Renderer[] ClipPlaneCastAll(Vector3 from, Quaternion orientation, float maxDistance, int layerMask = ~0)
    {
        var direction = orientation * -Vector3.forward;

        RaycastHit[] hitinfos = Physics.BoxCastAll(from, halfExtends, direction, orientation, maxDistance - ncpDistance, layerMask);

        Renderer[] renderers = new Renderer[hitinfos.Length];

        for (int i = 0; i < hitinfos.Length; i++)
        {
            renderers[i] = hitinfos[i].collider.GetComponent<Renderer>();
        }

        return renderers;
    }
}
