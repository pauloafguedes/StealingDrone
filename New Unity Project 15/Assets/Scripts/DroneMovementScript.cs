using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneMovementScript : MonoBehaviour {

    enum DanoSofrido
    {
        FULL_HEALTH,
        HALF_HEALTH,
        LOW_HEALTH
    }

    Rigidbody Drone;
    DanoSofrido LifeAtual;
    
    public float upForce;
    public float speed = 200.0f;
    public float halfspeed = 140.0f;
    public float lowspeed = 100.0f;
    public GameObject DroneBase;
    public LevelManager lvl;

    //variaveis para controle da vida
    public Image healthbar;
    public Text showhealth;
    float health;
    float maxhealth = 100;
    float damage = 10;


    void Start()
    {
        Drone = GetComponent<Rigidbody>();
        LifeAtual = DanoSofrido.FULL_HEALTH;
        health = maxhealth;
    }

    void LateUpdate()
    {
        float quantHealth = health / maxhealth ; //Ordem  health/max health(criar)
        healthbar.rectTransform.localScale = new Vector3(quantHealth, 1, 1);
        showhealth.text = (quantHealth*100).ToString() + '%';       

    }

    private void FixedUpdate()
    {
        MovementSides();
        MovementUpDown();
        Rotation();
        ClampingSpeedValues();
        //Usado para criar as animações do drone(melicopter) via programação
        // e também para não gerar falha na sua movimentação para os eixos X e Z
        DroneBase.transform.rotation = Quaternion.Euler(new Vector3(tiltZValueSides, droneYRotation, tiltXValueSides));
        Drone.transform.rotation = Quaternion.Euler(new Vector3(0, droneYRotation, 0));
    }


    //float movementSidesSpeed = 200.0f;
    float tiltZValueSides = 0;
    float tiltZSpeedSides;
    float tiltXValueSides = 0;
    float tiltXSpeedSides;
    void MovementSides() //movimento sobre os eixos X e Z
    {
        switch (LifeAtual)
        {
            case DanoSofrido.FULL_HEALTH:
                if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    Drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * speed + Vector3.right * Input.GetAxis("Horizontal") * speed);
                }

                break;

            case DanoSofrido.HALF_HEALTH:
                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    Drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * halfspeed + Vector3.right * Input.GetAxis("Horizontal") * halfspeed);
                }
                break;

            case DanoSofrido.LOW_HEALTH:
                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    Drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * lowspeed + Vector3.right * Input.GetAxis("Horizontal") * lowspeed);
                }
                break;

            default:
                break;
        }
        //inputVertical = Input.GetAxis("Vertical"); //movimenta sobre o eixo Z
        //inputHorizontal = Input.GetAxis("Horizontal"); //movimenta sobre o eixo X
     /*   if (Input.GetAxis("Vertical") != 0)
        {
            if(Input.GetAxis("Vertical")>0)
            {
                Drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * speed);
            }
            else
            {
                Drone.AddRelativeForce(Vector3.back * Input.GetAxis("Vertical") * -speed);
            }
            //Drone.AddRelativeForce(Vector3.up * 20);//mudar este campo pode tornar o gameplay bem mais interresante
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                Drone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * speed);
            }
            else 
            {
                Drone.AddRelativeForce(Vector3.left * Input.GetAxis("Horizontal") * -speed);
            }
            //Drone.AddRelativeForce(Vector3.up * 20); //mudar este campo pode tornar o gameplay bem mais interresante
        }
*/
        
        tiltZValueSides = Mathf.SmoothDamp(tiltZValueSides, 20 * Input.GetAxis("Vertical"), ref tiltZSpeedSides, 0.15f);
        tiltXValueSides = Mathf.SmoothDamp(tiltXValueSides, -20 * Input.GetAxis("Horizontal"), ref tiltXSpeedSides, 0.15f);
    }

    void MovementUpDown() //movimento sobre o eixo Y
    {
        Vector3 nextPos = Drone.transform.position;
        switch (LifeAtual)
        {
            case DanoSofrido.FULL_HEALTH:
                if (Input.GetMouseButton(0))
                {
                    upForce = 5.0f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                    lvl.GetTerrainHeightAt(nextPos);

                }
                if (Input.GetMouseButton(1))
                {
                    upForce = -5.0f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                }
                break;

            case DanoSofrido.HALF_HEALTH:
                if (Input.GetMouseButton(0))
                {
                    upForce = 4.0f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                    lvl.GetTerrainHeightAt(nextPos);

                }
                if (Input.GetMouseButton(1))
                {
                    upForce = -4.0f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                }
                break;

            case DanoSofrido.LOW_HEALTH:
                if (Input.GetMouseButton(0))
                {
                    upForce = 2f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                    lvl.GetTerrainHeightAt(nextPos);

                }
                if (Input.GetMouseButton(1))
                {
                    upForce = -2.0f;
                    Drone.AddRelativeForce(Vector3.up * upForce);
                    //codigo para nao ultrapassar o chao
                }
                break;

            default:
                break;
        }
        
    //   if (Input.GetMouseButton(0))
    //   {
    //       upForce = 100.0f;
    //       Drone.AddRelativeForce(Vector3.up * upForce);
    //       lvl.GetTerrainHeightAt(nextPos);
    //
    //   }
    //   if(Input.GetMouseButton(1))
    //   {
    //       upForce = -100.0f;
    //       Drone.AddRelativeForce(Vector3.up * upForce);
    //       //codigo para nao ultrapassar o chao
    //    }
        float terrainY = lvl.GetTerrainHeightAt(nextPos);
        if (nextPos.y < terrainY)
        {
            nextPos.y = terrainY;
            Debug.Log("Bateu no chao");
        }
        Drone.transform.position = nextPos;
    }

    float changeYRotation;
    public float droneYRotation;
    float rotatespeed = 8.5f;
    float rotationYSpeed;
    void Rotation()//gera uma rotação ao redor do eixo Y
    {
        //!=0 deixa o codigo mais simplificado
        //pode ser criado uma dificuldade onde ao ter halfhealth, girar o drone o faz cair um pouco
        if(Input.mouseScrollDelta.y!=0)
        {
            changeYRotation += (Input.mouseScrollDelta.y *rotatespeed);
        }
    //    if(Input.mouseScrollDelta.y > 0)
    //    {
    //        changeYRotation += rotatespeed;
    //    }
    
        droneYRotation = Mathf.SmoothDamp(droneYRotation, changeYRotation, ref rotationYSpeed, 0.25f);
    }
       
    private Vector3 velocitySmoothDampZero;
    void ClampingSpeedValues()//gera desaceleração sobre os movimentos nos eixos X e Z
    {
        //float str; perguntar novamente pois não foi absorvido direito a ideia(Confusão basica com valores entre 0 e 1)
        // compactar todos esses, usar uma variavel str= inputgetaxises, usar um check na variavel str
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            Drone.velocity = Vector3.ClampMagnitude(Drone.velocity, Mathf.Lerp(Drone.velocity.magnitude, 10.0f, Time.deltaTime * 5.0f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            Drone.velocity = Vector3.ClampMagnitude(Drone.velocity, Mathf.Lerp(Drone.velocity.magnitude, 10.0f, Time.deltaTime * 5.0f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            Drone.velocity = Vector3.ClampMagnitude(Drone.velocity, Mathf.Lerp(Drone.velocity.magnitude, 10.0f, Time.deltaTime * 5.0f));
        }

        // esse precisa manter
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            Drone.velocity = Vector3.SmoothDamp(Drone.velocity, Vector3.zero,ref velocitySmoothDampZero, 0.95f);
        }

    }

    public void SetLevelManager(LevelManager level)
    {
        lvl = level;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag=="Chao" || coll.gameObject.tag=="Item")
        {
            
            Debug.Log("Vel relative" + coll.relativeVelocity.magnitude);
            if(coll.relativeVelocity.magnitude>1.5f) //melhorar como ele recebe dano
            {                
                TakeDamage();
            }
            if (coll.relativeVelocity.magnitude > 0.3f) //melhorar como ele recebe dano
            {
                LowTakeDamage();
            }

        }
    }

    public float GetHealth()
    {
        return healthbar.rectTransform.localScale.x;
    }

    void LowTakeDamage()
    {
        health -= 2.0f;
        if (health <= 50)
        {
            LifeAtual = DanoSofrido.HALF_HEALTH;
            Debug.Log("Chegou na metade da vida");
        }
        if (health <= 20)
        {
            LifeAtual = DanoSofrido.LOW_HEALTH;
            Debug.Log("Está com a vida muito baixa");
        }
    }

    void TakeDamage()
    {
        health -= damage;
        if (health<= 50)
        {
            LifeAtual = DanoSofrido.HALF_HEALTH;
            Debug.Log("Chegou na metade da vida");
        }
        if(health<=20)
        {
            LifeAtual = DanoSofrido.LOW_HEALTH;
            Debug.Log("Está com a vida muito baixa");
        }       
        
    }
}
