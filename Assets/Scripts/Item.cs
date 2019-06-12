using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField] string nome;
    [SerializeField] DispararSom disparadorSom;
    [SerializeField] LevelManager lvl;

    //[SerializeField] AudioSource som;
    [SerializeField] AudioClip somclip;
    //[SerializeField] AudioClip somquebraclip;


    [SerializeField] bool estouGrudado;
    [SerializeField] bool valioso;
    [SerializeField] bool quebravel;
    [SerializeField] bool comestivel;

    [SerializeField] float valor;


    void Start()
    {
        lvl = FindObjectOfType<LevelManager>();
        nome = gameObject.name;
        estouGrudado = false;
    }
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collenter)
    {
        if (collenter.gameObject.tag == "Player")
        {
            //o valor de collenter.relativeVelocity.magnitude neste projeto é de 13,6
            float paraquebrar = collenter.relativeVelocity.magnitude;
            float causousom = this.GetComponent<Rigidbody>().mass * 5;
            //Debug.Log(paraquebrar);
            if(paraquebrar > 2  && quebravel)
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);                
                Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
                //Debug.Log("quebrou o item");
                
            }
            else if(paraquebrar>0.6)
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);
                Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);               
            }
            
            
            //precisa fazer checks de velocidade, para checar possivel quebra dos item
            
        }
        if (collenter.gameObject.tag == "Chao")
        {            
            float paraquebrar = collenter.relativeVelocity.magnitude;
            float causousom = this.GetComponent<Rigidbody>().mass * 5;
            Debug.Log("vel: "+ paraquebrar);

            if (paraquebrar > (5.0f) && quebravel) //este comparaçao para a quebra do objeto com o chao está mal feita
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);                
                Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);
                //Debug.Log("quebrou o item");
                Destroy(this.gameObject);
                
            }
            else if (paraquebrar > 2.0f)
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);                
                 Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);              
            }         
        }


        if (collenter.gameObject.tag=="Item")
        {
            float paraquebrar, causousom;
            if (collenter.gameObject.GetComponent<Rigidbody>())
            {
                paraquebrar = collenter.relativeVelocity.magnitude * collenter.gameObject.GetComponent<Rigidbody>().mass;
                causousom = this.GetComponent<Rigidbody>().mass * 5;
            }
            else
            {
                paraquebrar = 0;
                causousom = 0;
            }

            Debug.Log(paraquebrar);

            if(paraquebrar>4)
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);
                Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);
                //Debug.Log("quebrou o item");
                Destroy(this.gameObject);
            }
            else if (paraquebrar > 1.0f)
            {
                lvl.morador.SetSetaAcordar(causousom);
                disparadorSom.SetAudio(somclip);
                Instantiate(disparadorSom.gameObject, this.transform.position, this.transform.rotation);
            }

            //Debug.Log("Choque entre itens");
            
        }
        if (collenter.gameObject.tag=="Cachorro")
        {
            if(comestivel)
            {
                //som.PlayOneShot(somclip);              
            }
        }

        //aqui deve ser executado os checks com as tags e caso necessario checa sua velocidade para verificar quebra
    }

    void OnCollisionStay(Collision collstay)
    {
        if(collstay.gameObject.tag=="Player")
        {

            estouGrudado = true;
        }

        if(collstay.gameObject.tag=="Chao")
        {
            //Debug.Log("Estou parado no chao");
        }
        //aqui fica o check se o objeto ainda está grudado
        //pode apenas exibir um texto dizendo qual objeto está attach
    }

    void OnCollisionExit(Collision collexit)
    {
        estouGrudado = false;
        //aqui diz q o objeto está livre
    }

    public bool GetValioso()
    {
        return valioso;
    }    
}
    

