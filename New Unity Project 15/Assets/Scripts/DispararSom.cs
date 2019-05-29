using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararSom : MonoBehaviour {

    [SerializeField] AudioSource som;
    //[SerializeField] SphereCollider rangeSom;
    float volume;

	// Use this for initialization
	void Start ()
    {
        //rangeSom = GetComponent<SphereCollider>();
        Destroy(this.gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}


    public void SetAudio(AudioClip audio)
    {
        som.clip = audio;        
    }

    //public void SetRangeCollider(float f)
    //{
    //    rangeSom.radius= f;
    //}
}
