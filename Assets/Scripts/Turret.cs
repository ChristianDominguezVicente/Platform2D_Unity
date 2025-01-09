using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject disparo; //Prefab.
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private float velocidadPatrulla;
    private Vector3 destinoActual;
    private int indiceActual = 0;
    private bool jugadorEnRango;

    // Start is called before the first frame update
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            jugadorEnRango = true;
            StartCoroutine(RutinaAtaque());
        }
    }
    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            jugadorEnRango = false;
            StopCoroutine(RutinaAtaque());
        }
    }

    IEnumerator RutinaAtaque()
    {
        while (jugadorEnRango)
        {
            GameObject bola = Instantiate(disparo, puntoSpawn.position, Quaternion.Euler(0, 0, 90));
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }

    IEnumerator Patrulla() //S = V * t
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }
    }
    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
    }
}
