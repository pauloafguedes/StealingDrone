using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segurar : MonoBehaviour {

    public Rigidbody segura;
    bool grudou,chao;
    GameObject outro;
    Collider coli;
    Item item;

    //variaveis para o som
    public AudioClip glassbreaking;

    private AudioSource source;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        
        grudou = false;
        chao = false;
        segura = GetComponent<Rigidbody>();
        coli = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {

        ChecaVelocidadeY();

        if(grudou == true && Input.GetKey(KeyCode.E))
        {
            segura.isKinematic = true;
            coli.isTrigger = true;
            this.transform.SetParent(outro.transform);
            chao = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            segura.isKinematic = false;
            coli.isTrigger = false;
            transform.parent =null;
            grudou = false;
        }


        //colocar aqui um if duplo
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("Segurou");
            grudou = true;
            outro = other.gameObject;                        
        }
        if(other.gameObject.tag=="Chao")
        {
            chao = true;            
        }
    }

    

    //variaveis para checar quebra do item
    public float VelY;
    bool quebrou = false;
    void ChecaVelocidadeY()
    {
        if (segura != null)
        {
            VelY = segura.velocity.y;
            //Debug.Log("está com velocidade Y: " + VelY);
            //Debug.Log(VelY);
            if (VelY < -20)
            {
                quebrou = true;
                
            }
            if (chao == true && quebrou==true)
            {
                source.PlayOneShot(glassbreaking,1F); //não esta emitindo o som aqui
                Debug.Log("Quebrou");
                
                Destroy(gameObject);
            }

        }
    }
    //existe um bug q o colisor do item nao deixa o jogador se movimentar direito
}
