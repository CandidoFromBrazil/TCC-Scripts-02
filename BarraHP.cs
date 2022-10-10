using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraHP : MonoBehaviour
{
    public RectTransform barraHP;
    public PlayerMovement Player;

    private float hpMax;
    private float barraMaxWidth;
    // Start is called before the first frame update
    void Start()
    {
        hpMax = Player.life;
        barraMaxWidth = barraHP.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        barraHP.sizeDelta = new Vector2(CalculaHP(), barraHP.rect.height);
    }

    public float CalculaHP()
    {
        return (Player.life * barraMaxWidth) / hpMax;
    }
}
