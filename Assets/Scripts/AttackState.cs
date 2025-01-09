using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float timeBetweenAttacks;
    
    private Transform target;

    private float timer = 0;
    private float danhoAtaque = 20;
    private Player player;

    private Animator anim;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        timer = timeBetweenAttacks;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAttacks)
        {
            anim.SetTrigger("atacar");
            timer = 0f;  
        }
        if (Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            controller.ChangeState(controller.ChaseState);
        }
    }

    public override void OnExitState()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            target = player.transform;
        }
    }

    private void Danho()
    {
        SistemaVidas sistemaVidas = player.gameObject.GetComponent<SistemaVidas>();
        sistemaVidas.RecibirDanho(danhoAtaque);
    }

}
