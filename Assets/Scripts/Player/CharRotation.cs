using UnityEngine;

public class CharRotation : MonoBehaviour
{
    Vector3 mousePosition;
    public LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen space
        mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
        {
            Vector3 pointToLook = hitInfo.point;

            Vector3 direction = pointToLook - transform.position;
            direction.y = 0;

            direction = direction * -1;

            Debug.DrawRay(transform.position, direction);

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
