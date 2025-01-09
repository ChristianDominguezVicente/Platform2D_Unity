using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monitor : MonoBehaviour, IInteractuable
{
    [SerializeField] private TMP_Text textAdvertencia;
    [SerializeField] private string[] textos;

    private float time = 5;

    public void Interactuar()
    {
        StartCoroutine(ActiveText(time));
    }
    private IEnumerator ActiveText(float time)
    {
        textAdvertencia.gameObject.SetActive(true);
        foreach (string texto in textos)
        {
            textAdvertencia.text = texto;
            yield return new WaitForSeconds(time);
        }
        textAdvertencia.gameObject.SetActive(false);
    }
}
