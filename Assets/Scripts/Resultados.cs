using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resultados : MonoBehaviour
{
    [SerializeField] private GameObject Fin;
    [SerializeField] private TMP_Text Puntuacion;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Vida <= 0)
        {
            MostrarResultados();
        }
    }

    public void MostrarResultados()
    {
        if (Fin.activeSelf) return;

        Puntuacion.text = "Monedas: " + player.Monedas;
        Fin.SetActive(true);
        Time.timeScale = 0f;
        if (player.Monedas > PlayerPrefs.GetInt("Monedas", 0))
        {
            PlayerPrefs.SetInt("Monedas", (int)player.Monedas);
        }
    }

    public void VolverAJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");
        PlayerPrefs.SetInt("SalirJuego", 1);
    }
}
