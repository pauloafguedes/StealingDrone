using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttach : MonoBehaviour {

    [SerializeField] GameObject Garra;    
    [SerializeField] bool podeagarrar;   

    bool attached;

    void Start()
    {
        podeagarrar = false;
    }
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag=="Item")
        {
            podeagarrar = true;
            float itemMaisProximo = Mathf.Infinity;
            Item ItemProximo = null;
            Item[] todosItens = GameObject.FindObjectsOfType<Item>();

            foreach(Item atualItem in todosItens)
            {
                float distanciaItem = (atualItem.transform.position - this.transform.position).sqrMagnitude;
                if(distanciaItem< itemMaisProximo)
                {
                    itemMaisProximo = distanciaItem;
                    ItemProximo = atualItem;
                }
            }

            Debug.DrawLine(this.transform.position, ItemProximo.transform.position);
            if (Input.GetKey(KeyCode.E) && podeagarrar)
            {              
                Debug.Log("Juntou Objetos");
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.GetComponent<Collider>().isTrigger = true;
                ItemProximo.transform.SetParent(this.transform);
                attached = true;
                //AttachParent(other.gameObject);
            }
            if (Input.GetKey(KeyCode.Q) && attached)
            {
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Collider>().isTrigger = false;
                other.gameObject.transform.parent = null;
                attached = false;
            }
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        podeagarrar = false;
    }



 //   public void AttachParent(GameObject newParent)
 //   {        
 //       newParent.transform.parent = Garra.transform;        
 //   }
 //
 //   void DetachParent(GameObject newParent)
 //   {
 //       newParent.transform.parent = null;       
 //   }

}
