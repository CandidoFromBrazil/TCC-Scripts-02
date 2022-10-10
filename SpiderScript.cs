using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    Animation anim; //VARIÁVEL PARA CHAMAR UMA ANIMAÇÃO

    //ataque
    private BoxCollider areaAtk; //--------------
    private bool atacando = false; //--------------
    public float distanciaAtk = 15f; //--------------

    //persegue
    public float MaxDistance = 30f; //LIMITE MÁXIMO DO CAMPO DE VISÃO DA ARANHA
    private float ActualDistance; //DISTACIA ATUAL ENTRE A ARANHA E O PLAYER
    public Transform player; //VARIÁVEL PARA INSTANCIAR PLAYER
    public float SpiderSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation> (); //AO COMEÇAR O JOGO, VAMOS CHAMAR (anim) PEGANDO O COMPONENTENTE DA ANIMAÇÃO
        areaAtk = GetComponent<BoxCollider> (); //COMANDO PEGANDO ALGUM COMPONENTE DENTRO DA UNITY (DO OBJETO COM O SCRIPT) //--------------
        player = GameObject.FindGameObjectWithTag ("Player").transform; // INSTANCIANDO (player) PELA SUA TAG ("Player")
    }

    // Update is called once per frame
    void Update()
    {
        atacando = anim.IsPlaying("Attack"); //--------------
        areaAtk.enabled = anim.IsPlaying("Attack"); //--------------
        ActualDistance = Vector3.Distance(transform.position, player.transform.position); //RELAÇÃO DE MEDIDA DA DISTANCIA ATUAL ENTRE A ARANHA E O PLAYER 
        if (ActualDistance <= distanciaAtk)
        {
            Atacar();
        }else
        if (ActualDistance <= MaxDistance){ //CONDIÇÃO DE VALIDAÇÃO DE DISTANCIA
            transform.LookAt (player); // PARA OO OGBEJCT(SPIDER) OLHAR PARA A VARIÁVEL PLAYER
            transform.position += transform.forward * SpiderSpeed * Time.deltaTime; //L. DE CÓDIGO PARA QUE A ARANHA MOVA EM DIREÇÃO AO PLAYER
            anim.Play("Walk"); //CHAMANDO A ANIMAÇÃO (Walk) PARA QUANDO A ARANHA ESTIVER ANDANDO
        }else { //QUANDO A OPERAÇÃO DE A OPERAÇÃO ENTRE: ActualDistance <= MaxDistance, FOR FOR >=, CAI NO ELSE, DO IFELSE
            anim.Play("Idle"); // ANIMAÇÃO (Idle) PARA O ELSE
        }
    }

    void Atacar()
    {
        if (!atacando)
        {
            atacando = true;
            anim.Play("Attack");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
