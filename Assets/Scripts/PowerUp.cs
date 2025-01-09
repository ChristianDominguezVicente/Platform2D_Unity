using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float amplitud;
    [SerializeField] private float frecuencia;

    private Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud, posicionInicial.z);
    }
}
