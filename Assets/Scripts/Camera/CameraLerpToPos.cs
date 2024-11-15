using UnityEngine;

public class CameraLerpToPos : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    private Vector3 startPosition;
    private Vector3 endPosition = new(0, 16, -16);
    private float elapsedTime = 0f;
    private bool isMoving = false;


    private void Awake()
    {
        GameObject.Find("StartBtn").GetComponent<StartBtn>().onGameStart += StartMove;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float easedT = Mathf.SmoothStep(0, 1, t);

            transform.position = Vector3.Lerp(startPosition, endPosition, easedT);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }

    public void StartMove()
    {
        elapsedTime = 0f;
        isMoving = true;
    }
}
