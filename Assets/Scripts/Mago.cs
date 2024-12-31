using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{
    [SerializeField] private GameObject bolaFuego; //Prefab.
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private float danhoAtaque;
    private Animator anim;
    private Transform jugador;
    private bool jugadorEnRango;
    private Vector2 ultimaPosicionJugador;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnRango && jugador != null)
        {
            Vector3 direccionJugador = jugador.position - transform.position;
            if (direccionJugador.x > 0)
                transform.localScale = Vector3.one;
            else
                transform.localScale = new Vector3(-1, 1, 1);
            ultimaPosicionJugador = jugador.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            jugador = elOtro.transform;
            jugadorEnRango = true;
            ultimaPosicionJugador = jugador.position;
            StartCoroutine(RutinaAtaque());
        }
    }
    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            jugador = null;
            jugadorEnRango = false;
            StopCoroutine(RutinaAtaque());
        }
    }
    IEnumerator RutinaAtaque()
    {
        while (jugadorEnRango)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }
    private void LanzarBola()
    {
        Vector2 objetivo;
        if (jugador != null)
            objetivo = jugador.position;
        else
            objetivo = ultimaPosicionJugador;

        GameObject bola = Instantiate(bolaFuego, puntoSpawn.position, Quaternion.identity);
        Vector2 direccion = (objetivo - (Vector2)puntoSpawn.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        bola.transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
}
