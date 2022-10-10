using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : MonoBehaviour
{

    int life = 100;

    float money = 250.0f;

    bool isAttacking = false;

    string nome = "Gui";

    // Start is called before the first frame update
    void Start()
    {
        Status();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Hunt()
    {
        print("Estou Caçando...");
        //Debug.log("estou caçando");
        life -= 20;
        money += 100;
        isAttacking = true;
    }

    void Sleep()
    {
        print("Estou Dormindo...");
        life += 50;
        money -= 200;
        isAttacking = false;
    }

    void Status()
    {
        print("Status Personagem");
        print("Nome: " + nome);
        print("Life: " + life);
        print("isAttacking: " + isAttacking);
        
    }
}
