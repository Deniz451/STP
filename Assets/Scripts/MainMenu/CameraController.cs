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
    private Transform camera;


    private void Start()
    {
        camera = Camera.main.transform;
    }

    public void Update()
    {
        if (lerp && targetPosition != null)
        {
            camera.SetPositionAndRotation(
                Vector3.Lerp(camera.position, targetPosition.position, Time.deltaTime * speed), 
                Quaternion.Lerp(camera.rotation, targetPosition.rotation, Time.deltaTime * speed));

            if (Vector3.Distance(camera.position, targetPosition.position) < 0.01f &&
                Quaternion.Angle(camera.rotation, targetPosition.rotation) < 0.1f)
            {
                camera.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
                lerp = false;
            }
        }
    }


    public void LerpCameraPos(float speed, CameraPoints position)
    {
        this.speed = speed;

        switch(position)
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
