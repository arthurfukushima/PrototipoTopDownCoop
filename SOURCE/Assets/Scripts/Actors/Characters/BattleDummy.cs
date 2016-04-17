using UnityEngine;
using System.Collections;

public class BattleDummy : BaseAI
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
}
