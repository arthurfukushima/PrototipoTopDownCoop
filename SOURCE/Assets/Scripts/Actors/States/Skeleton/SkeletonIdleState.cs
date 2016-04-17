using UnityEngine;
using System.Collections;

public class SkeletonIdleState : BaseIdleState
{
    public override void Reason()
    {
        context.ChangeState<SkeletonRunState>();
    }

    public override void Update(float pDeltaTime)
    {
    }
}
