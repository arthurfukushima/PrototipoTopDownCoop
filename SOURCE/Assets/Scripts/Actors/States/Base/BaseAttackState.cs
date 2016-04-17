using UnityEngine;
using System.Collections;
using System;

public class BaseAttackState : SKState<BaseCharacter>
{
    private Vector3 direction;
    private float maxSpeed;

    public override void OnInitialized()
    {
        base.OnInitialized();

        context._AnimationEventsController.CreateEvent("Attack_01", OnAttackAnimationEvent);
        context._AnimationEventsController.CreateEvent("AttackRanged_01", OnAttackRangedAnimationEvent);
    }

    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_ATTACK, 0.1f);
    }

    public override void Reason()
    {
        base.Reason();

        direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }

    public override void Update(float pDeltaTime)
    {
        base.Update(pDeltaTime);

        maxSpeed = context._GameplayController.movementSpeed;

        context.Move(direction, maxSpeed * 0.8f);
        context.UpdateRotation();
    }

    private void OnAttackAnimationEvent()
    {
        context._AttackController.AttackAsSphere(context, context.transform.position, 1.0f, 3);

        context.ChangeState<BaseIdleState>();
    }

    private void OnAttackRangedAnimationEvent()
    {
        context._AttackController.AttackRanged(context, 10, context.transform.position + context.transform.forward + Vector3.up, 
                                               context.transform.forward, context._GameplayController.projectileSpeed);

        context.ChangeState<BaseIdleState>();
    }

    public override void OnGUI()
    {
        base.OnGUI();

        GUILayout.Box("Master: " + "RUN");
    }
}
