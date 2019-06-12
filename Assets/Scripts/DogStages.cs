using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DogStages : MonoBehaviour {

    //variaveis para controle da vida
    //public Image dogStage;
    //public UnityEngine.UI.Text percent;
    float furious;
    float maxfurious = 100.0f;
    [SerializeField]float increase = 10.0f;
    [SerializeField] protected float COOLDOWN_TIME = 2.0f;
    

    protected float timeToIncrement;

    // Use this for initialization
    void Start () {
        furious = 0.0f;
        timeToIncrement = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {
               
    }

    public void FuriousDog()
    {
        //furious += increase;
        if (furious >= 0 && furious<50 && Time.time >=this.timeToIncrement)
        {
            furious += increase;
            this.timeToIncrement = Time.time + COOLDOWN_TIME;        
        Debug.Log("Está andando");
        }


        if (furious >=50 && furious<100 && Time.time >=this.timeToIncrement)
        {
            furious += increase*2;
            this.timeToIncrement = Time.time + COOLDOWN_TIME;
            this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed =1.0f;
            
            Debug.Log("Está correndo");
        }
        if (furious == 100)
        {
            //quando chega neste nivel a variavel de barulho deve subir muito rápido para gerar o gameOver por som excessivo
            //neste momento não vai ser necessario checar se o morador está perto do som dom latido
            Debug.Log("Começa a latir");            
        }
    }

    public float GetDogStage()
    {
        return furious;
    }
}
