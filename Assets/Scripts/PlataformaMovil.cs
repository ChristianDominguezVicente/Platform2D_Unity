using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidadMovimiento;
    private int indiceActual = 1;
    private bool ordenPlataformas = true;

    void Update()
    {
        if (ordenPlataformas && indiceActual + 1 >= waypoints.Length)
            ordenPlataformas = false;

        if (!ordenPlataformas && indiceActual<= 0)
            ordenPlataformas = true;

        if(Vector2.Distance(transform.position, waypoints[indiceActual].position) < 0.1f)
        {
            if (ordenPlataformas)
                indiceActual += 1;
            else
                indiceActual -= 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[indiceActual].position, velocidadMovimiento * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.transform.SetParent(null);
        }
    }
}
