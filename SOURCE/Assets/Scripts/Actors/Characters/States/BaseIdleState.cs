using UnityEngine;
using System.Collections;

public class BaseIdleState : SKState<BaseCharacter>
{
    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_IDLE, 0.1f);
    }

    public override void Reason()
    {
        base.Reason();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);

        if(direction != Vector3.zero || direction != Vector3.zero)
        {
            context.ChangeState<BaseRunState>();
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
        base.Update(pDeltaTime);

        context.UpdateRotation();
    }

    public override void OnGUI()
    {
        base.OnGUI();

        GUILayout.Box("Master: " + "IDLE");
    }
}
