using UnityEngine;
using System.Collections;

public class BattleDummy : BaseCharacter
{
    public override SKStateMachine<BaseCharacter> _StateMachine {
        get {
            if(stateMachine == null)
            {
                stateMachine = new SKStateMachine<BaseCharacter>(this, new DummyIdleState());
                stateMachine.AddState(new BaseDamageState());
            }

            return stateMachine;
        }
    }
}
