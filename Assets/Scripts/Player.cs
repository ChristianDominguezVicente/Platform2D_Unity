using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    [Header("Sistema de movimiento")]
    [SerializeField] private Transform pies;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private LayerMask queEsSaltable;

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private LayerMask queEsDanhable;

    [Header("Sistema de interaccion")]
    [SerializeField] private Transform puntoDeteccion;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private LayerMask queEsInteractuable;

    [Header("Sistema de meta")]
    [SerializeField] private GameObject Fin;
    [SerializeField] private TMP_Text Puntuacion;
    [SerializeField] private float delay;

    [Header("PowerUp")]
    [SerializeField] private float cura;
    [SerializeField] private float tiempo;

    private Animator anim;
    private bool flagDobleSalto = false;
    private bool flagSaltoHecho = false;

    private float vida;
    private float monedas = 0;

    public float Vida { get { return vida; } }
    public float Monedas { get { return monedas; } set { monedas = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SistemaVidas sistemaVidas = gameObject.GetComponent<SistemaVidas>();
        vida = sistemaVidas.Vida;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Saltar();
        LanzarAtaque();
        Interactuar();
    }

    private void Interactuar()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Collider2D collDetectado = Physics2D.OverlapCircle(puntoDeteccion.position, radioDeteccion, queEsInteractuable);
            if (collDetectado != null)
            {
                if (collDetectado.TryGetComponent(out IInteractuable interactuable))
                {
                    interactuable.Interactuar();
                }
            }
        }
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("attack");
        }
    }

    //Se ejecuta desde evento de animación.
    private void Ataque()
    {
        //LANZAR ESE TRIGGER INSTANTANEO.
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (EstoyEnSuelo())
            {
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                anim.SetTrigger("saltar");
                flagSaltoHecho = false;
            }
            else if (flagDobleSalto && !flagSaltoHecho)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                anim.SetTrigger("saltar");
                flagSaltoHecho = true;
            }
            
        }
    }

    private bool EstoyEnSuelo()
    {
        Debug.DrawRay(pies.position, Vector3.down, Color.red, 0.3f);
        return Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidadMovimiento, rb.velocity.y);
        if (inputH != 0) //Hay movimiento
        {
            anim.SetBool("running", true);
            if (inputH > 0) //Derecha
            {
                transform.eulerAngles = Vector3.zero;
            }
            else //Izquierda
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else //inputH = 0.
        {
            anim.SetBool("running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Meta"))
        {
            StartCoroutine(Meta(delay));
        }
        else if (elOtro.gameObject.CompareTag("Caida"))
        {
            StartCoroutine(Caida(delay));
        }
        else if (elOtro.gameObject.CompareTag("Life"))
        {
            SistemaVidas sistemaVidas = gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.Curar(cura);
            Destroy(elOtro.gameObject);
        }
        else if (elOtro.gameObject.CompareTag("DJump"))
        {
            Destroy(elOtro.gameObject);
            StartCoroutine(DJump(tiempo));
        }
    }

    private IEnumerator Meta(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (monedas > PlayerPrefs.GetInt("Monedas", 0))
        {
            PlayerPrefs.SetInt("Monedas", (int)monedas);
        }

        if (monedas > PlayerPrefs.GetInt("Monedas"))
            Puntuacion.text = "Monedas: " + monedas;
        else
            Puntuacion.text = "Monedas: " + PlayerPrefs.GetInt("Monedas");
        Fin.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator Caida(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (monedas > PlayerPrefs.GetInt("Monedas"))
            Puntuacion.text = "Monedas: " + monedas;
        else
            Puntuacion.text = "Monedas: " + PlayerPrefs.GetInt("Monedas");
        Fin.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator DJump(float delay)
    {
        flagDobleSalto = true;
        yield return new WaitForSeconds(delay);
        flagDobleSalto = false;
    }
}
