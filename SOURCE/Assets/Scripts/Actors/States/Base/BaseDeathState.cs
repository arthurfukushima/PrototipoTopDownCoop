using UnityEngine;
using System.Collections;

public class BaseDeathState : SKState<BaseCharacter>
{
    public override void Begin()
    {
        base.Begin();

        context._AnimationController.CrossFadeInFixedTime(AnimatoHash.FULLBODY_DEATH, 0.1f);

        context._PhysicsController.CachedCollider.enabled = false;
        context._PhysicsController.CachedRigidbody.isKinematic = true;
        context._PhysicsController.CachedRigidbody.useGravity = false;
        context._PhysicsController.Velocity = Vector3.zero; 
    }

    public override void Reason()
    {
        base.Reason();

        if(timeOnState > 3.0f)
            context.OnFinishedDeath();
    }
}
