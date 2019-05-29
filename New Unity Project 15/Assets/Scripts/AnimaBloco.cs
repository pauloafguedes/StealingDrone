using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimaBloco : MonoBehaviour {

    Animator bloconotas;
    bool blocoaberto;
    

    //public Text informacoesobtidas;
    //static List<string> anotacoes; //nao será uma lista de String, e provavelmente uma lista de TextUI
    [SerializeField] List<Text> EscritaRoubo;
    [SerializeField] List<Item> ItensParaRoubar;
    [SerializeField] List<GameObject> Checkers;

    private void Awake()
    {
        for (int i = 0; i < ItensParaRoubar.Count; i++)
        {
            EscritaRoubo[i].text = ItensParaRoubar[i].name;//precisa adicionar na escrita o nome do Pai do ItensParaRoubar
        }
    }

    public int RetornaCount()
    {
        return ItensParaRoubar.Count;
    }
    
    public void AtivaCheckers(int x)
    {
        Checkers[x].gameObject.SetActive(true);
    }

    public string RetornaNomeItem(int x)
    {
        return ItensParaRoubar[x].name;
    }
    // Use this for initialization
    void Start () {
        
        
        bloconotas = GetComponent<Animator>();
        
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && blocoaberto!=true)
        {
            AbreBloco();
            blocoaberto = true;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && blocoaberto == true)
        {
            FechaBloco();
            blocoaberto = false;
        }
    }

    /// <summary>
    /// Essa função é chamada toda vez ue um level é iniciado
    /// </summary>
    /// <param name="level">o numero do level carregado</param>
    

    public void AbreBloco()
    {
        Debug.Log(33);

        bloconotas.SetBool("movebloco", true);
    }
    public void FechaBloco()
    {
        Debug.Log(22);
        bloconotas.SetBool("movebloco", false);
    }
    
    

    //public void AtualizaAnotacoes() //pode ser chama no código da lixeira para ativar o Checked(gameobject criado dentro do bloco) no Item Roubado
    //{
    //    informacoesobtidas.text = string.Empty;
    //    foreach (string anotacao in anotacoes)
    //    {
    //        informacoesobtidas.text += anotacao + "\n";
    //    }
    //    
    //}
    //
    //public void EscrevenoBloco(string novaAnotacao)
    //{
    //    anotacoes.Add(SceneManager.GetActiveScene().name + " - "+ novaAnotacao);
    //    AtualizaAnotacoes();        
    //}
    //
    //public void LimpaAnotacoes()
    //{
    //    anotacoes.Clear();
    //}

    
}
