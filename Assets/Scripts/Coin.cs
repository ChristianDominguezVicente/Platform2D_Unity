using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractuable
{
    private Player player;
    public void Interactuar()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Monedas += 1;
        TMP_Text textPuntuacion = (TMP_Text)GameObject.Find("Puntos").GetComponent<TMP_Text>();
        textPuntuacion.text = "Monedas: " + player.Monedas;

        Destroy(gameObject);
    }
}
