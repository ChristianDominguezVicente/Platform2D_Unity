using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    [SerializeField] private GameObject menuInicio;
    [SerializeField] private GameObject menuOpciones;
    [SerializeField] private GameObject menuNiveles;
    [SerializeField] private TMP_Text nivel1;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SalirJuego") == 1)
        {
            menuInicio.SetActive(false);
            menuOpciones.SetActive(false);
            menuNiveles.SetActive(true);
            PlayerPrefs.SetInt("SalirJuego", 0);
        }
        else
            PlayerPrefs.SetInt("SalirJuego", 0);

        if (PlayerPrefs.HasKey("Monedas"))
            nivel1.text = "" + PlayerPrefs.GetInt("Monedas");
    }
    public void Jugar1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    public void CargarMenuOpIni()
    {
        PlayerPrefs.SetInt("Menu", 0);
    }

    public void CargarMenuOpNi()
    {
        PlayerPrefs.SetInt("Menu", 1);
    }
}
