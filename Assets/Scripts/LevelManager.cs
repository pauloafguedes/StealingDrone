using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject chao;
    public DroneMovementScript Drone;
    public Morador morador;
    public DogStages dogstage;

    [SerializeField] UnityEngine.UI.Text showTempo;

    [SerializeField] float limiteTempo;
    float horaInicio;
        
    void Start()
    {
        horaInicio = Time.time;
    }
     

    void Update()
    {
        this.Drone.SetLevelManager(this);
        this.morador.SetLevelManager(this);
        
        if(morador.GetNivelAcordar()>1)
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Acordou morador, GameOver");
        }
        if(Drone.GetHealth()==0)
        {
            Debug.Log("Você quebrou o drone");
            Destroy(Drone.gameObject);
            SceneManager.LoadScene("GameOver");
        }
        if(dogstage.GetDogStage()==100)
        {
            Debug.Log("Cachorro comeca a latir");
            morador.FuriaMaxima();
        }


        float tempoRestante = limiteTempo - (Time.time - horaInicio);
        //Debug.Log(horaInicio);

        if (tempoRestante > 0)
        {
            showTempo.text = "Bateria: " + Relogio(tempoRestante);
        }
        else
        {
            showTempo.text = "Bateria: 00:00 ";
            SceneManager.LoadScene("GameOver");
        }

        if (tempoRestante > 0)
        {
            return;
        }
        else
        {
            showTempo.text = "Bateria: --:--";
            
        }
    }

    //void LateUpdate()
    //{
    //    Vector3 pos = chao.transform.position;
    //    pos.y = Terrain.activeTerrain.SampleHeight(chao.transform.position);
    //    chao.transform.position = pos;
    //}

    public float GetTerrainHeightAt(Vector3 pos)
    {
        
        return chao.transform.position.y;
    }


    public string Relogio(float temporestante)
    {
        int tempo = (int)temporestante;
        int minutos = tempo / 60;
        int segundos = tempo % 60;

        if (segundos < 10)
        {
            string relogio = ("0" + minutos + ":0" + segundos);
            return relogio;
        }
        else
        {
            string relogio = ("0" + minutos + ":" + segundos);
            return relogio;
        }

    }
}
