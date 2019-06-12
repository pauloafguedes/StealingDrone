using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionarHelice : MonoBehaviour
{
    public GameObject[] Helices;

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < Helices.Length; i++)
        //{
        //    Helices[i] = gameObject.GetComponentInChildren<GameObject>();
        //}
    }
       

    // Update is called once per frame
    void Update()
    {
        RotacionaHelice(Helices, 0);
        RotacionaHelice(Helices, 1);
        RotacionaHelice(Helices, 2);
        RotacionaHelice(Helices, 3);
    }

    void RotacionaHelice(GameObject[] h,int x)//gera as rotações da hélices
    {
        Quaternion targetpropeller = Quaternion.Euler(0, 3000 * Time.deltaTime, 0);        
        h[x].transform.localRotation = targetpropeller * h[x].transform.localRotation;        
    }
}