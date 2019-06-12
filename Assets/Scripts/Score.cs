using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField] List<string> nome;
    [SerializeField] List<float> valor;

    string adnome;
    float advalor;

    public Text showNomeEscrever;

    // Use this for initialization
    void Start () {
        
        adnome = Console.ReadLine();
        showNomeEscrever.text = adnome;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
