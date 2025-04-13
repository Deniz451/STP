using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    Vector3 moveDirection = new Vector3(0, 0, 0);
    bool move = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.y = 0;

        if (player != null)
        {
            moveDirection.x = player.position.x - gameObject.transform.position.x;
            moveDirection.z = player.position.z - gameObject.transform.position.z;

            if(player.position != gameObject.transform.position) { move = true; }   
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            gameObject.transform.position += moveDirection / 10;
        }
    }
}
