using UnityEngine;
using System.Collections;

public class BasePlayer : BaseCharacter
{
    public bool hasControl;

    public override SKStateMachine<BaseCharacter> _StateMachine 
    {
        get {
            if(stateMachine == null)
            {
                stateMachine = new SKStateMachine<BaseCharacter>(this, new BaseIdleState());
                stateMachine.AddState(new BaseRunState());
                stateMachine.AddState(new BaseAttackState());
                stateMachine.AddState(new BaseRollState());
                stateMachine.AddState(new BaseDamageState());
                stateMachine.AddState(new BaseSpecialSkill());
            }

            return stateMachine;
        }
    }

    protected override void Start()
    {
        base.Start();

        PlayersControlManager.Instance.RegisterPlayer(this);
        ActorsManager.Instance.RegisterPlayer(this);
    }

    protected override void Update()
    {
        if(hasControl)
            _StateMachine.Update(Time.deltaTime);
    }
}
