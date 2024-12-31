using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State<EnemyController>
{
    private Transform target;
    [SerializeField] private float chaseVelocity;
    [SerializeField] private float stoppingDistance;

    private Animator anim;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        anim = GetComponent<Animator>();
        anim.SetTrigger("perseguir");
    }
    public override void OnUpdateState()
    {
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseVelocity * Time.deltaTime);
            if(Vector3.Distance(transform.position, target.position) <= stoppingDistance)
            {
                controller.ChangeState(controller.AttackState);
            }
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            controller.ChangeState(controller.PatrolState);
        }
    }
}
