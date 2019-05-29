using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour {

    bool lockCursor;
    [SerializeField] float sense = 10.0f;
    [SerializeField] Transform target;
    [SerializeField] float distFromTarget = 2;
    Vector2 pitchMinMax = new Vector2(-40, 85);

    [SerializeField] float rotSmoothTime = 8.0f;
    Vector3 rotSmoothVel;
    Vector3 currentRot;

    float yaw;
    float pitch;

    //transparencia
    public bool changeTransparency = true;
    public MeshRenderer targetRenderer;

    //speeds
    public float moveSpeed = 5.0f;
    public float returnSpeed = 9.0f;
    public float wallPush = 0.7f;

    //distances
    public float evenCloserDistanceToPlayer =1.0f;
    public float closesDistanceToPlayer = 2.0f;

    //Mask
    [SerializeField] LayerMask collisionMask;

    bool pitchLock = false;


	// Use this for initialization
	void Start ()
    {
	    if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {

        CollisionCheck(target.position - transform.forward * distFromTarget);
        WallCheck();

        if (!pitchLock)
        {
            yaw += Input.GetAxis("Mouse X") * sense;
            pitch -= Input.GetAxis("Mouse Y") * sense;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRot = Vector3.Lerp(currentRot, new Vector3(pitch, yaw), rotSmoothTime * Time.deltaTime);
        }
        else
        {
            yaw += Input.GetAxis("Mouse X") * sense;
            pitch = pitchMinMax.y;
            currentRot = Vector3.Lerp(currentRot, new Vector3(pitch, yaw), rotSmoothTime * Time.deltaTime);

        }

        //currentRot = Vector3.SmoothDamp(currentRot,new Vector3(pitch,yaw),ref rotSmoothVel, rotSmoothTime);

        transform.eulerAngles = currentRot;

        Vector3 aux = transform.eulerAngles;
        aux.x = 0;

        target.eulerAngles = aux;

	}

    void CollisionCheck(Vector3 retPoint)
    {
        RaycastHit hit;

        if(Physics.Linecast(target.position, retPoint,out hit, collisionMask))
        {
            Vector3 norm = hit.normal * wallPush;
            Vector3 p = hit.point + norm;

            TranparencyCheck();

            if(Vector3.Distance (Vector3.Lerp (transform.position, p, moveSpeed * Time.deltaTime), target.position) <= evenCloserDistanceToPlayer)
            {

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            }
            return;

        }

        FullTranparency();

        transform.position = Vector3.Lerp(transform.position, retPoint, moveSpeed * Time.deltaTime);
        pitchLock = false;
    }

    void TranparencyCheck()
    {
        if(changeTransparency)
        {
            if(Vector3.Distance(transform.position,target.position) <= closesDistanceToPlayer)
            {

                Color temp = targetRenderer.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 0.2f, moveSpeed * Time.deltaTime);

                targetRenderer.sharedMaterial.color = temp;
                
            }
            else
            {
                if(targetRenderer.sharedMaterial.color.a <= 0.99f)
                {
                    Color temp = targetRenderer.sharedMaterial.color;
                    temp.a = Mathf.Lerp(temp.a, 1.0f, moveSpeed * Time.deltaTime);

                    targetRenderer.sharedMaterial.color = temp;
                }
            }
        }
    }
    void FullTranparency()
    {
        if(changeTransparency)
        {
            if (targetRenderer.sharedMaterial.color.a <= 0.99f)
            {
                Color temp = targetRenderer.sharedMaterial.color;
                temp.a = Mathf.Lerp(temp.a, 1.0f, moveSpeed * Time.deltaTime);

                targetRenderer.sharedMaterial.color = temp;
            }
        }
    }

    void WallCheck()
    {
        Ray ray = new Ray(target.position, -target.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray, 0.5f, out hit, 0.7f, collisionMask))
        {
            pitchLock = true;
        }
        else
        {
            pitchLock = false;
        }
    }
}
