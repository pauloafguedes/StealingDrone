using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoubado : MonoBehaviour {

    
    public UnityEngine.UI.Text showcontador;
    [SerializeField]AnimaBloco anima;
    int contador = 0;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        showcontador.text = "Itens Recolhidos: " + contador;
        if (contador==1)
        {
            Debug.Log("Jogador Recolheu todos os itens");
        }

	}

    void OnCollisionEnter(Collision col) //possivel lugar errado
    {
        if(col.gameObject.tag=="Item" && col.gameObject.GetComponent<Item>().GetValioso()!=false)
        {
            for(int i=0; i<anima.RetornaCount() ;i++)
            {
                if (col.gameObject.GetComponent<Item>().name.Equals(anima.RetornaNomeItem(i)))
                {
                    Debug.Log("Bateu o item certo");
                    anima.GetComponent<AnimaBloco>().AtivaCheckers(i);
                    col.gameObject.SetActive(false);
                    contador++;
                }
            }
            
            
        }
    }
}
