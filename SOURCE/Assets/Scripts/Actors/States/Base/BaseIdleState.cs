using UnityEngine;
using System.Collections;

public class BaseIdleState : SKState<BaseCharacter>
{
    public override void OnInitialized()
    {
        base.OnInitialized();

        context.onReceivedDamageCallback += OnReceivedDamage;
    }

    public void OnReceivedDamage(BaseActor pBully, int pDamage)
    {
        context.ChangeState<BaseDamageState>();
    }
    
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

//        if(Input.GetButtonDown("Fire1"))
        if(Input.GetButtonDown("Attack"))
        {
            context.ChangeState<BaseAttackState>();
            return;
        }

        if(Input.GetButtonDown("Special"))
        {
            context.ChangeState<BaseSpecialSkill>();
            return;
        }

        if(Input.GetButtonDown("Dodge"))
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
}
