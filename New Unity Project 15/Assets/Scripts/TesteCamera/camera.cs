using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public float cameraMoveSpeed = 120.0f;
    public GameObject cameraFollowObj;
    Vector3 followPOS;
    public float clampangle = 80.0f;
    public float inputsense = 150.0f;
    //public GameObject cameraOBJ;
    //public GameObject playerOBJ;
    //public float camdistXToPlayer;
    //public float camdistYToPlayer;
    //public float camdistZToPlayer;
    public float mouseX;
    public float mouseY;
    //public float finalInputX;
    //public float finalInputZ;
    //float smoothX;
    //float smoothY;
    float rotY = 0.0f;
    float rotX = 0.0f;


    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        //float inputX = Input.GetAxis("Horizontal");
        //float inputZ = Input.GetAxis("Vertical");
        mouseX = -Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");        

        rotY += + mouseX * inputsense * Time.deltaTime;
        rotX += + mouseY * inputsense * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, 0,clampangle);

        Quaternion localRot = Quaternion.Euler(rotX, rotY , 0.0f);
        transform.rotation = localRot;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = cameraFollowObj.transform;

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        
        

    }
}

