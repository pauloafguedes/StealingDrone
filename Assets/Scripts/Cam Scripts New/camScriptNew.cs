using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScriptNew : MonoBehaviour {
    [SerializeField] GameObject PlayerReference;
    public float lerpSpeed = 0;

	// Use this for initialization
	void Start () {
        this.transform.position = PlayerReference.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.MoveTowards
            (transform.position, PlayerReference.transform.position,
            lerpSpeed*(PlayerReference.transform.position - transform.position).magnitude *Time.deltaTime);

        transform.forward = Vector3.Lerp(transform.forward, PlayerReference.transform.forward, 1*lerpSpeed*Time.deltaTime);
		
		
	}
}
