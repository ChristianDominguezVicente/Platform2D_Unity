using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;

    private Animator anim;
    private bool destroyFlag = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RecibirDanho(float danhoRecibido)
    {
        if (destroyFlag) return;

        vidas -= danhoRecibido;
        if(vidas <= 0)
        {
            destroyFlag = true;
            anim.SetTrigger("explosion");
            float duracion = anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(Destruccion(duracion));
        }
    }

    private IEnumerator Destruccion(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
