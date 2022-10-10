using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float delayJump = 0.3f;
    public float walkSideSpeed = 3;
    public float walkReverseSpeed = 2;
    public float rotateSpeed = 200;
    public float jumpStrength = 0.4f;
    public Vector3 walkAcceleration = new Vector3(0, 0, 0);
    float jumpAcceleration = 0;
    int jumpCount = 0;
    public CharacterController playerController;
    public Animator playerAnimator;
    public float life;
    public float hpmax;
    public  Image BarraHp;

    public Transform bulletSpawnPoint;
    public GameObject BulletPrefab;
    public float BulletSpeed = 10;


    private BoxCollider areaAtk;
    public GameObject areaDano;
    // Start is called before the first frame update
    void Start()
    {
        life = 100f;
        life = hpmax;
        playerController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        //areaAtk = areaDano.GetComponent<BoxCollider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInputs();
        Movements();
        UpdateUI();

        playerAnimator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        playerAnimator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetMouseButtonDown(0))
        {
            var Bullet = Instantiate(BulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * BulletSpeed;
        }
        if (life == 0)
        {
            SceneManager.LoadScene("Lose2");
        }
    }


    void Movements()
    {
       
        // Horizontol movements 
        playerController.Move(walkAcceleration * Time.deltaTime);
        // Vertical movements
        playerController.Move(new Vector3(0, jumpAcceleration, 0));

        // Rotation
        playerController.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);

        // Horizontal deceleration
        if (playerController.isGrounded)
        {
            walkAcceleration = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            walkAcceleration = Vector3.MoveTowards(walkAcceleration, Vector3.zero, Time.deltaTime);
        }
        // Vertical deceleration
        if (jumpAcceleration > -0.98f)
        {
            jumpAcceleration = Mathf.MoveTowards(jumpAcceleration, -0.98f, Time.deltaTime * 2);
        }
    }

    void KeyboardInputs()
    {
         if (Input.GetKey(KeyCode.W))
        {
            // Prevent player to fly away like bullet when not on ground
            if (playerController.isGrounded)
            {
                walkAcceleration += playerController.transform.forward * walkSpeed;
            }
            else
            {
                walkAcceleration = playerController.transform.forward * walkSpeed;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Prevent player to fly away like bullet when not on ground
            if (playerController.isGrounded)
            {
                walkAcceleration += playerController.transform.right * -walkSideSpeed;
            }
            else
            {
                walkAcceleration = playerController.transform.right * -walkSideSpeed;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            // Prevent player to fly away like bullet when not on ground
            if (playerController.isGrounded)
            {
                walkAcceleration += playerController.transform.forward * -walkReverseSpeed;
            }
            else
            {
                walkAcceleration = playerController.transform.forward * -walkReverseSpeed;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            // Prevent player to fly away like bullet when not on ground
            if (playerController.isGrounded)
            {
                walkAcceleration += playerController.transform.right * walkSideSpeed;
            }
            else
            {
                walkAcceleration = playerController.transform.right * walkSideSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Double jump
            if (jumpCount < 2)
            {

                jumpCount += 1;
                playerAnimator.SetBool("Jump", true);
                Invoke ("DelayJump", delayJump);
                Invoke ("StopJump", 0.1f);

            }

        }
    }

    void DelayJump()
    {
        jumpAcceleration = jumpStrength;
    }

    void StopJump()
    {
        playerAnimator.SetBool("Jump", false);
    }


    public void TirarHP(float dano)
    { //Função que precisa de um parametro (valor no dano)
        life -= dano; //HP - valor do dano recebido
    }

    void OnTriggerEnter(Collider colisor)
    { //Função da Unity que recebe o collider com nome de colisor(parametro)
        if (colisor.gameObject.tag == "Spider")
        { //Se o objeto que colidiu com você chamado colisor ter a tag Spider
            TirarHP(25);// Chama a função TirarHp atribuindo 25 ao Dano 
        }

        if(colisor.gameObject.tag == "Win")
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void UpdateUI()
    {
        BarraHp.fillAmount = life / hpmax;
    }
}
