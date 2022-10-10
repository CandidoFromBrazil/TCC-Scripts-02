using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject telapause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MudarEstadoPause();
        }
    }

    public void MudarEstadoPause()
    {
        if(Time.timeScale == 0) //Se o momento do jogo estiver parado
        {
            Time.timeScale = 1; //timeScale = 1 é para o jogo fluir normalmente 
            telapause.SetActive(false);
        }else {
            Time.timeScale = 0;
            telapause.SetActive(true);
        }
    }
}
