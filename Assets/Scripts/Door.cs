using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractuable
{
    [SerializeField] private float cantidad;
    [SerializeField] private TMP_Text textAdvertencia;

    private Player player;
    private float time = 3;
    public void Interactuar()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player.Monedas >= cantidad)
        {
            Destroy(gameObject);
        }
        else 
        {
            StartCoroutine(ActiveText(time));
        }
    }

    private IEnumerator ActiveText(float time)
    {
        textAdvertencia.gameObject.SetActive(true);
        textAdvertencia.text = "Se necesitan " + cantidad + " Monedas para abrir la puerta";
        yield return new WaitForSeconds(time);
        textAdvertencia.gameObject.SetActive(false);
    }
}
