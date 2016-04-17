﻿using UnityEngine;
using System.Collections;

public class Skeleton : BaseAI
{
    public override SKStateMachine<BaseCharacter> _StateMachine {
        get {
            if(stateMachine == null)
            {
                stateMachine = new SKStateMachine<BaseCharacter>(this, new DummyIdleState());
                stateMachine.AddState(new BaseDamageState());
                stateMachine.AddState(new BaseDeathState());
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
}