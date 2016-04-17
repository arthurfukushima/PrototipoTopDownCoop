using UnityEngine;
using System.Collections;

public class BaseDamageState : SKState<BaseCharacter>
{
    public override void OnInitialized()
    {
        base.OnInitialized();

        context.onReceivedDamageCallback += OnReceivedDamage;
    }

    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_DAMAGE, 0.1f);
    }

    public override void Reason()
    {
        base.Reason();

        if(timeOnState > 0.5f)
        {
            context.ChangeState<BaseIdleState>();
            return;
        }
    }

    public void OnReceivedDamage(BaseActor pBully, int pDamage)
    {
        if(isCurrentState)
        {
            context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_DAMAGE, 0.1f);
            timeOnState = 0;
        }
    }
}
