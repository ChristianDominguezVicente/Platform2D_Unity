using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private GameObject Fin;
    [SerializeField] private float vidas;
    [SerializeField] private TMP_Text Puntuacion;

    private Animator anim;
    private bool destroyFlag = false;

    private BarraVida barraVida;

    private Player player;

    public float Vida { get { return vidas; } }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        if (gameObject.CompareTag("PlayerHitbox"))
        {
            barraVida = GameObject.Find("BarraVida").GetComponent<BarraVida>();
            barraVida.SetMaxHealth(vidas);
        }
    }

    public void RecibirDanho(float danhoRecibido)
    {
        if (destroyFlag) return;

        vidas -= danhoRecibido;

        if (gameObject.CompareTag("PlayerHitbox"))
        {
            barraVida.SetHealth(vidas);
        }

        if (vidas <= 0)
        {
            destroyFlag = true;
            anim.SetTrigger("explosion");
            float duracion = anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(Destruccion(duracion));

        }
    }

    public void Curar(float cura)
    {
        if (destroyFlag) return;

        if (gameObject.CompareTag("PlayerHitbox") && vidas < 100)
        {
            vidas += cura;
            barraVida.SetHealth(vidas);
        }
    }

    private IEnumerator Destruccion(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (gameObject.CompareTag("PlayerHitbox"))
        {
            if (player.Monedas > PlayerPrefs.GetInt("Monedas"))
                Puntuacion.text = "Monedas: " + player.Monedas;
            else
                Puntuacion.text = "Monedas: " + PlayerPrefs.GetInt("Monedas");
            Fin.SetActive(true);
            Time.timeScale = 0f;
        }
        Destroy(this.gameObject);
    }
}
