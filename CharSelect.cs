using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    public GameObject PlayerGreen;
    public GameObject PlayerRed;

    public GameObject BarraHP;
    public GameObject Menu;

    public void SelecttPlayer(GameObject Player)
    {
        Instantiate (Player, new Vector3(23f, 2f, 5f), transform.rotation);
        DesativaMenu();
    }

    public void DesativaMenu()
    {
        BarraHP.SetActive(true);
        Menu.SetActive(false);
    }
}
