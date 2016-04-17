using UnityEngine;
using System.Collections;
using System;

public class BaseRunState : SKState<BaseCharacter>
{
    private Vector3 direction;
    private float maxSpeed;

    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_RUN, 0.1f);
    }

    public override void Reason()
    {
        base.Reason();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0.0f, vertical);

        if(direction == Vector3.zero && direction == Vector3.zero)
        {
            context.ChangeState<BaseIdleState>();
            return;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            context.ChangeState<BaseAttackState>();
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            context.ChangeState<BaseRollState>();
            return;
        }
    }

    public override void Update(float pDeltaTime)
    {
        maxSpeed = context._GameplayController.movementSpeed;

        base.Update(pDeltaTime);

        context.Move(direction, maxSpeed);
        context.UpdateRotation();
    }

    public override void OnGUI()
    {
        base.OnGUI();

        GUILayout.Box("Master: " + "RUN");
    }
}
