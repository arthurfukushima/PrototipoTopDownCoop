using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skeleton : BaseAI
{
    public override SKStateMachine<BaseCharacter> _StateMachine {
        get {
            if(stateMachine == null)
            {
                stateMachine = new SKStateMachine<BaseCharacter>(this, new SkeletonIdleState());
                stateMachine.AddState(new SkeletonRunState());
                stateMachine.AddState(new BaseDamageState());
                stateMachine.AddState(new BaseDeathState());
                stateMachine.AddState(new BaseKnockbackState());
            }

            return stateMachine;
        }
    }

    public override SpawnPool _OnDeathFinishedFXPool {
        get {
            if(onDeathFXPool == null)
                onDeathFXPool = PoolManager.Instance.GetPool("Skeleton_OnDeath_FX_Pool");

            return onDeathFXPool;
        }
    }

    public override void OnFinishedDeath()
    {
        base.OnFinishedDeath();

        gameObject.SetActive(false);
    }

    public BasePlayer GetClosestPlayer()
    {
        List<BasePlayer> players = ActorsManager.Instance.GetPlayers();
        BasePlayer target = null;

        float minDist = 999;

        foreach(BasePlayer player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance < minDist)
            {
                minDist = distance;
                _Blackboard.currentTarget = player;
                target = player;
            }
        }

        return target;
    }
}
