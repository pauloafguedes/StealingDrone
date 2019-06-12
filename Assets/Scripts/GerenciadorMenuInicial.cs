using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GerenciadorMenuInicial : MonoBehaviour {

    [SerializeField] AudioMixer audioMixer;


    public GameObject goMenuInicial;
    public GameObject goOpcoes;
    [SerializeField] GameObject goControles;
    bool OpcoesAberto = false;
    bool ControlesAberto = false;
    [SerializeField] GameObject select;
    [SerializeField] GameObject selectControl;
    [SerializeField] GameObject selectInicial;
    

    void Update()
    {
           
        if (ControlesAberto)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                goControles.SetActive(false);
                ControlesAberto = false;
                OpcoesAberto = true;
                goOpcoes.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                goOpcoes.SetActive(false);
                OpcoesAberto = false;
                goMenuInicial.SetActive(true);
                
            }
        }
    }

    public void BotaoIniciar()
    {

        Debug.Log("Clicou Iniciar");        
        SceneManager.LoadScene("casa");
    }

    public void BotaoOpcoes()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(select, new BaseEventData(eventSystem));
        goMenuInicial.SetActive(false);
        goOpcoes.SetActive(true);
        OpcoesAberto = true;

    }

    public void BotaoCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    
    public void BotaoSair()
    {
        Debug.Log("Clicou Sair");
        Application.Quit();
    }
    
    public void MostraControles()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectControl, new BaseEventData(eventSystem));
        goControles.SetActive(true);
        goOpcoes.SetActive(false);
        OpcoesAberto = false;
        ControlesAberto = true;
    }

    public void BotaoVoltar()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectInicial, new BaseEventData(eventSystem));
        Debug.Log("volta menu anterior");
        goMenuInicial.SetActive(true);
        goOpcoes.SetActive(false);
        OpcoesAberto = false;    
    }

    public void BotaoVoltarControle()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(select, new BaseEventData(eventSystem));
        goControles.SetActive(false);
        goOpcoes.SetActive(true);
        OpcoesAberto = true;
        ControlesAberto = false;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    
    public void SetFullScreen(bool FullScreen)
    {
        Screen.fullScreen = FullScreen;
    }
}
