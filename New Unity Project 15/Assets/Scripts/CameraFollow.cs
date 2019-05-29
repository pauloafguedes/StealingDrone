using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform Drone;

	void Start ()
    {
        Drone = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private Vector3 CameraFollowVelocity;
    public Vector3 PositionCamera = new Vector3(0, 2, -4);
    public float angle;
	void FixedUpdate ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Drone.transform.TransformPoint(PositionCamera) + Vector3.up * Input.GetAxis("Vertical"), ref CameraFollowVelocity, 0.2f);
        transform.rotation = Quaternion.Euler(new Vector3(angle, Drone.GetComponent<DroneMovementScript>().droneYRotation, 0));
        
	}
}
