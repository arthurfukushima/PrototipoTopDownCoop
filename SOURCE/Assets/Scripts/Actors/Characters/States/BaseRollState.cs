using UnityEngine;
using System.Collections;

public class BaseRollState : SKState<BaseCharacter>
{
    private Vector3 direction;
    private float maxSpeed;

    public override void Begin()
    {
        base.Begin();

        if(context._GameplayController.rollToMouseDirection)
        {
            direction = (context.RaycastMousePosition() - context.transform.position).normalized;
            direction.y = 0.0f;
        }
        else
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if(direction == Vector3.zero)
                direction = context.transform.forward;
        }

        Debug.DrawRay(context.transform.position, direction, Color.blue, 3f);
    }

    public override void Reason()
    {
        base.Reason();

        if(timeOnState > context._GameplayController.rollDuration)
        {
            context._PhysicsController.Velocity = Vector3.zero;
            context.ChangeState<BaseIdleState>();
            return;
        }
    }

    public override void Update(float pDeltaTime)
    {
        base.Update(pDeltaTime);

        maxSpeed = context._GameplayController.movementSpeed;

        context.Move(direction, maxSpeed * context._GameplayController.rollSpeedMultiplier);
    }

    public override void OnGUI()
    {
        base.OnGUI();

        GUILayout.Box("Master: " + "ROLL");
    }
}
