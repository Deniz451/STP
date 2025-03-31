using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraPoints
    {
        Default,
        PartSelection,
        GunLInspect,
        GunRInspect
    }

    public Transform[] cameraPoints;
    private Transform targetPosition;
    private float speed;
    private bool lerp = false;
    private Transform cam;
    private Camera camCom;
    private float? targetSize = 5.5f;


    private void Start()
    {
        cam = Camera.main.transform;
        camCom = cam.GetComponent<Camera>();
    }

    public void Update()
    {
        if (lerp && targetPosition != null)
        {
            cam.SetPositionAndRotation(
                Vector3.Lerp(cam.position, targetPosition.position, Time.deltaTime * speed), 
                Quaternion.Lerp(cam.rotation, targetPosition.rotation, Time.deltaTime * speed));

            if (targetSize.HasValue)
            {
                camCom.orthographicSize  = Mathf.Lerp(camCom.orthographicSize, targetSize.Value, Time.deltaTime * 5f);
            }

            if (Vector3.Distance(cam.position, targetPosition.position) < 0.01f &&
               (Quaternion.Angle(cam.rotation, targetPosition.rotation) < 0.1f) &&
               (!targetSize.HasValue || Mathf.Abs(camCom.orthographicSize - targetSize.Value) < 0.01f))
            {
                cam.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
                if (targetSize.HasValue) camCom.orthographicSize = targetSize.Value;
                lerp = false;
                targetSize = null;
            }
        }
    }


    public void LerpCameraPos(float speed, CameraPoints position, float? newSize = null)
    {
        this.speed = speed;
        targetSize = newSize;

        switch (position)
        {
            case CameraPoints.Default:
                targetPosition = cameraPoints[0];
                lerp = true;
                break;
            case CameraPoints.PartSelection:
                targetPosition = cameraPoints[1];
                lerp = true;
                break;
            case CameraPoints.GunLInspect:
                targetPosition = cameraPoints[2];
                lerp = true;
                break;
            case CameraPoints.GunRInspect:
                targetPosition = cameraPoints[3];
                lerp = true;
                break;
        }
    }
}
