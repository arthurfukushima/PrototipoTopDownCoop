using UnityEngine;
using System.Collections;

public class SkeletonIdleState : BaseIdleState
{
    private Skeleton skeletonContext;

    public override void OnInitialized()
    {
        base.OnInitialized();
        skeletonContext = (Skeleton)context;
    }

    public override void Reason()
    {
        if(skeletonContext.GetClosestPlayer() != null)
        {
//            direction = (skeletonContext._Blackboard.currentTarget - context.transform.position).normalized;
            context.ChangeState<SkeletonRunState>();
        }
//        else
//            context.ChangeState<SkeletonIdleState>()
    }

    public override void Update(float pDeltaTime)
    {
    }
}
