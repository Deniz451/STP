using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharRotation : MonoBehaviour
{
    Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = new Vector3(mousePosition.x - transform.position.x, 0, mousePosition.z - transform.position.z);
        transform.rotation = Quaternion.AngleAxis(Quaternion.FromToRotation(transform.position, mousePosition).y, Vector3.up);
    }
}
