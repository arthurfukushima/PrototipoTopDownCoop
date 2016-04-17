using UnityEngine;
using System.Collections;

public class BaseKnockbackState : SKState<BaseCharacter>
{
    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CachedAnimator.applyRootMotion = true;

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_KNOCKBACK, 0.1f);

//        context._AnimationController.CachedAnimator.bodyPosition
    }

    public override void Reason()
    {
        base.Reason();

        if(timeOnState > 2.4f)
        {
            DebugExtension.DebugArrow(context._AnimationController.CachedAnimator.rootPosition, Vector3.up, 5);
            context.ChangeState<BaseIdleState>();
            return;
        }
    }

    public override void End()
    {
        base.End();

        context._AnimationController.CachedAnimator.applyRootMotion = false;
    }
}
