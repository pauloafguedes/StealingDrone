using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float turnSpeed = 15.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 14.0f, player.position.z - 15.0f);
    }

    void FixedUpdate()
    {
        //if (Input.GetMouseButton(0)) //este mousebutton é o scroll do mouse
        //{
        //    offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        //    //perguntar sobre a transformação para Vector3 quando multiplicado pelo *offset
        //}
        //transform.position = player.position + offset;
        //transform.LookAt(player.position);

        if (Input.mouseScrollDelta.y !=0 ) 
        {
            offset = Quaternion.AngleAxis(Input.mouseScrollDelta.y * turnSpeed, Vector3.up) * offset;
            
        }
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
