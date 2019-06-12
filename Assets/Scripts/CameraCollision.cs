using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    [SerializeField] float minDistance = 1.0f;
    [SerializeField] float maxDistance = 3.0f;
    [SerializeField] float smooth = 8.0f;

    Vector3 dollyDir;
    [SerializeField] Vector3 dollyDirAdjusted;
    [SerializeField] float distance;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }
    
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredCameraPos = transform.parent.TransformPoint (dollyDir * maxDistance);
        RaycastHit hit;

        if(Physics.Linecast (transform.parent.position,desiredCameraPos, out hit))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}
