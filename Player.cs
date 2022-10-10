using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  
    private Animator anim; //Aqui Criamos uma variavel do tipo Animator
    private CharacterController controller; //Criamos uma variavel do tipo Character controller

    public float speed = 8; //Criamos uma variavel do tipo float para armazenar a velocidade do player
    public float rotSpeed = 70; //Criamos uma variavel do tipo float para armazenar a velocidade do player
    private float gravity; //criamos uma variavel do tipo float para armazenar a força de gravidade do player

    private float rot;  //Criamos uma variavel do tipo float para armazenar a rotação do player
    private Vector3 moveDirection; //Controla  a direção do player vai andar

    public GameObject MainCamera; //Criamos uma variavel do tipo GameObject para armazenar a camera do jogo


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Pega o Animator Controller dos componentes do player
        controller = GetComponent<CharacterController>(); // Pega o CharacterControler dos componentes do player
        MainCamera = GameObject.Find("Main Camera"); //Pega o Main Camera dos componentes do player
        
    }

    // Update is called once per frame
    void Update()
    {

        Move(); //chama função de move
        
    }
    void Move()
    {
        //Controla o estado da animação
        //Configuramos nossa Animação de acordo com o paramentro Speed, usa o metodo para configurar na Vertical
        anim.SetFloat("speed",Input.GetAxis("Vertical"));

        if(controller.isGrounded) //Se o meu personagem estiver no chão
        {
            if(Input.GetButton("Vertical")) //Se eu pressionar o Input vertical  (W ou S)
            {
                moveDirection = Vector3.forward * speed * Input.GetAxis("Vertical"); //Adiociona no MoveDirection um Vector pra frente * Velocidade * input Vertical
            }

            if(Input.GetButtonUp("Vertical")) //Se eu soltar o Input Vertical (W ou S)
            {
                moveDirection = Vector3.zero; //Adiociona o moveDirection um Vector de 0 nos 3 Eixo
            }
       }
       else
       {
            moveDirection.y -= gravity * Time.deltaTime; //A força do eixo Y diminui com a força da Gravidade * o tempo de processamento
       }
       rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime; //rotação aumenta com o valor do Input Horizontal * Velocidade de rotação * tempo de Processamento
       transform.eulerAngles = new Vector3(0 , rot , 0);//Passa Um novo eixo de rotação para o player

       moveDirection = MainCamera.transform.TransformDirection(moveDirection); //Passamos a rotação Local (Do proprio Objeto) para o personagem ao inves a Global

       controller.Move(moveDirection * Time.deltaTime); //Controller Movimenta passando o Vector3 * tempo de processamento
    }
}
