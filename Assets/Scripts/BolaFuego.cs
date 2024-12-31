using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    [SerializeField] private float danhoAtaque;
    [SerializeField] private float tiempoDestruccion;
    private Animator anim;
    private bool destroyFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //transform.forward --> MI EJE Z. (AZUL)
        //transform.up --> MI EJE Y. (VERDE)
        //transform.right --> MI EJE X (ROJO)

        Vector2 direccion = transform.right;
        rb.AddForce(direccion * impulsoDisparo, ForceMode2D.Impulse);

        StartCoroutine(Temporizador());
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (destroyFlag) return;

        if (elOtro.gameObject.CompareTag("PlayerHitbox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);

            Animacion();
        }
    }

    private void Animacion()
    {
        if (destroyFlag) return;

        destroyFlag = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.SetTrigger("explotar");

        float duracion = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(Destruccion(duracion));
    }

    private IEnumerator Temporizador()
    {
        yield return new WaitForSeconds(tiempoDestruccion);
        if (!destroyFlag)
            Animacion();
    }

    private IEnumerator Destruccion(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
