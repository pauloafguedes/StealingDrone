using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogControl : MonoBehaviour {

    [SerializeField] NavMeshAgent agente;
    [SerializeField] GameObject som;
    [SerializeField] DogStages stage;
    [SerializeField] GameObject Drone;



    protected WaitForSeconds tempo; //provavel simplificação de código, pode tirar essa variavel

    [SerializeField] protected int i;
    //[SerializeField] protected int listTam;

    //protected Transform novoWayPoint;

    [SerializeField] List<Transform> waypoints;
    [SerializeField] List<float> waitTime;
    float probabilidadeTroca = 0.2f;
    [SerializeField] float tempoatual;

    [SerializeField] bool procurando;
    [SerializeField] bool esperandopatrulha;
    [SerializeField] bool esperando;
    [SerializeField] bool proxpatrulha;
    
    Vector3 destinoAtual;

    bool TemVisao = false;
    float dist; //nao sei se existe necessidade da existencia desta variavel

    // Use this for initialization
    void Start()
    {
        i = 0; //Random.Range(0, waypoints.Count);
        agente = GetComponent<NavMeshAgent>();
        //listTam = waypoints.Count - 1;
        tempo = new WaitForSeconds(waitTime[i]);
        dist = this.GetComponent<SphereCollider>().radius;
        SetDestination();
        
    }

    // Update is called once per frame
    void Update()
    {
        //agente.destination = destinoAtual;        

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, (Drone.transform.position - transform.position), out hit, dist))
        {
            
            if (hit.collider.tag == "Player")
            {
                Debug.Log("bateu com: " + hit.collider.tag);
                TemVisao = true;
                return;
            }
            TemVisao = false;
            esperandopatrulha = true;
        }

        if(TemVisao==false && procurando && agente.remainingDistance<=0.3f)
        {
            procurando = false;
            
            if(esperandopatrulha)
            {
                esperando = true;
                tempoatual = 0.0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if(esperando)
        {
            tempoatual += Time.deltaTime;
            if(tempoatual>= waitTime[i])
            {
                esperando = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }

        
    }

    void SetDestination()
    {
        if (waypoints !=null)
        {
            Vector3 nextPos = waypoints[i].transform.position;
            agente.SetDestination(nextPos);
            procurando = true;
        }
    }

    void ChangePatrolPoint()
    {
        if(Random.Range(0.0f,1.0f) <= probabilidadeTroca)
        {
            proxpatrulha = !proxpatrulha;
        }

        if(proxpatrulha)
        {
            i = (i + 1) % waypoints.Count; 
        }
        else
        {
            if(--i<0)
            {
                i = waypoints.Count - 1;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Som")
        {
            som = coll.gameObject;
            agente.SetDestination(coll.transform.position);
        }

        if (coll.gameObject.tag == "Player" && TemVisao == true)
        {
            agente.SetDestination(coll.transform.position);
            esperandopatrulha = false;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && TemVisao == true)
        {
            agente.SetDestination(coll.transform.position);
            stage.FuriousDog();
            esperandopatrulha = false;
        }

    }

    //void OnTriggerExit (Collider coll)
    //{
    //    if(coll.gameObject.tag == "Player")
    //    {
    //        esperandopatrulha = true;
    //    }
    //}
}
