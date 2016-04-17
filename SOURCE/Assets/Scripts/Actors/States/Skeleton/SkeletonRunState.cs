using UnityEngine;
using System.Collections;

public class SkeletonRunState : BaseRunState
{
    protected Vector3 direction;
    protected float maxSpeed;

    public override void OnInitialized()
    {
        context.onReceivedDamageCallback += OnReceivedDamage;
    }

    public override void Reason()
    {
        direction = context.transform.forward;
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
