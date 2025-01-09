using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    [SerializeField] private float duracion;
    [SerializeField] private float danhoAtaque;
    private bool destroyFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruccion(duracion));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (destroyFlag) return;

        if (elOtro.gameObject.CompareTag("PlayerHitbox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDanho(danhoAtaque);
            destroyFlag = true;
            Destroy(gameObject);
        }
    }
    private IEnumerator Destruccion(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!destroyFlag)
        {
            destroyFlag = true;
            Destroy(gameObject);
        }
    }
}
