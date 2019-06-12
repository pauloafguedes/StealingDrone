using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GerenciadorMenuGame : MonoBehaviour {

    [SerializeField] AudioMixer audioMixer;
    public GameObject goMenuInGame;
    public GameObject Menu;
    public GameObject goOpcoes;
    public GameObject Controles;
    static bool menuEstaAberto = false;
    static bool OpcoesAberto = false;
    bool ControlesAberto = false;

    void Awake()
    {
        goMenuInGame.SetActive(false);
        Menu.SetActive(false);
        Time.timeScale = 1f;
        menuEstaAberto = false;
    }
    void Update()
    {
        if (OpcoesAberto == false && ControlesAberto == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuEstaAberto)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    FechaMenu();
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    AbreMenu();
                }
            }
        }
        else if(ControlesAberto)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Controles.SetActive(false);
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
                Menu.SetActive(true);
            }
        }
             
    }

    private void AbreMenu()
    {
        goMenuInGame.SetActive(true);
        Menu.SetActive(true);
        Time.timeScale = 0f;
        menuEstaAberto = true;
    }

    public void FechaMenu()
    {
        goMenuInGame.SetActive(false);
        Menu.SetActive(false);
        Time.timeScale = 1f;
        menuEstaAberto = false;
    }

    public void Opcoes()
    {
        Menu.SetActive(false);
        goOpcoes.SetActive(true);
        OpcoesAberto = true;
    }
    
    public void MostraControles()
    {
        goOpcoes.SetActive(false);        
        ControlesAberto = true;
        Controles.SetActive(true);
    }
        
    public void BotaoMenuInicial()
    {   
        Debug.Log("Clicou Voltar Menu Inicial");
        SceneManager.LoadScene("MenuInicial");
    }

    public void BotaoQuitGame()
    {
        Debug.Log("Clicou Sair");
        Application.Quit();
    }

    public void BotaoVoltar()
    {
        Debug.Log("volta menu anterior");
        goOpcoes.SetActive(false);
        OpcoesAberto = false;
        Menu.SetActive(true);
        menuEstaAberto = true;
    }

    public void BotaoVoltarControle()
    {
        Controles.SetActive(false);
        ControlesAberto = false;
        goOpcoes.SetActive(true);
        OpcoesAberto = true;
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
