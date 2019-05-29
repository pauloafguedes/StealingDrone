using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Morador : MonoBehaviour {


    [SerializeField] float cooldownDiminuiAcordar = 10.0f;
    [SerializeField] float cooldownDecresceAcordar = 3.0f;
    [SerializeField] float setaAcordar =0; // esta variavel quando chegar a 100, aumenta NivelAcordar em 0,34
    [SerializeField] float NivelAcordar =0; //aumentar de 0,34 em 0,34, quando o valor passar de 100, decreta lose game por som
    [SerializeField] Image acordarbar;
    [SerializeField] protected float COOLDOWN_TIME = 2.0f;
    [SerializeField] protected float REDUZIR_ACORDAR = 10.0f;

    protected float timeToIncrement;
    protected float timeToDecrementAcordar;
    protected float PodeAbaixarAcordar;


    [SerializeField] LevelManager lvl;

    // Use this for initialization
    void Start()
    {
        timeToIncrement = 0.0f;
        timeToDecrementAcordar = 0.0f;
        PodeAbaixarAcordar = 10.0f;
    }
    void LateUpdate()
    {
        if(setaAcordar>=100)
        {
            setaAcordar = 0.0f;
            NivelAcordar += 0.34f;
            
        }
        if(setaAcordar>0 && Time.time>=PodeAbaixarAcordar)
        {
            if(Time.time >= timeToDecrementAcordar)
            {
                setaAcordar -= 2.0f;
                timeToDecrementAcordar = Time.time + COOLDOWN_TIME;
            }
            
        }
        acordarbar.rectTransform.localScale = new Vector3(NivelAcordar, 1, 1);
    }

    //private void OnTriggerEnter(Collider coll)
    //{
    //    if(coll.CompareTag("Som"))
    //    {
    //        if(setaAcordar>0)
    //        {
    //
    //        }
    //        Debug.Log("seta som no Acordar");
    //    }
    //}

    public void FuriaMaxima()
    {
        if(NivelAcordar<1 && Time.time >= this.timeToIncrement)
        {
            NivelAcordar += 0.1f;
            this.timeToIncrement = Time.time + COOLDOWN_TIME;
        }
        if(NivelAcordar>1)
        {
            NivelAcordar = 1.0f;
            SceneManager.LoadScene("GameOver");
        }
        
    }

    public void SetLevelManager(LevelManager level)
    {
        lvl = level;
    }

    public void SetSetaAcordar(float forcasom)
    {
        setaAcordar += forcasom;
        PodeAbaixarAcordar = Time.time + REDUZIR_ACORDAR;
    }
    
    public float GetNivelAcordar()
    {
        return acordarbar.rectTransform.localScale.x;
    }

    
	
	
	
}
