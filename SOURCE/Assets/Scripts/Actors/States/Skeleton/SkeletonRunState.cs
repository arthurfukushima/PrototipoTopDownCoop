using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkeletonRunState : BaseRunState
{
    private Skeleton skeletonContext;

    protected Vector3 direction;
    protected float maxSpeed;

    public override void OnInitialized()
    {
        context.onReceivedDamageCallback += OnReceivedDamage;

        skeletonContext = (Skeleton)context;

        float randRange = Random.Range(0.9f, 1.1f);
        context._GameplayController.movementSpeed *= randRange;
        context._AnimationController.SetFloat("movementSpeed", randRange);
    }

    public override void Reason()
    {
        if(skeletonContext.GetClosestPlayer() != null)
        {
            direction = (skeletonContext._Blackboard.currentTarget.transform.position - context.transform.position).normalized;
        }
        else
            context.ChangeState<SkeletonIdleState>();
//        direction = context.transform.forward;
    }

    public override void Update(float pDeltaTime)
    {
        maxSpeed = context._GameplayController.movementSpeed;

        context.Move(direction, maxSpeed);
        context.Rotate(context._PhysicsController.Velocity, maxSpeed);
    }

    public void OnReceivedDamage(BaseActor pBully, int pDamage)
    {
        context.ChangeState<BaseDamageState>();
    }
}
