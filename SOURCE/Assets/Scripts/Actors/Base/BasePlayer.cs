using UnityEngine;
using System.Collections;

public class BasePlayer : BaseCharacter
{
    protected bool hasControl;

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

    public bool _HasControl{
        get{
            return hasControl;
        }
        set{
            hasControl = value;

            if(!hasControl)
                _StateMachine.ChangeState<BaseIdleState>();
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

    public override void Move(Vector3 pDirection, float pSpeed)
    {
        Quaternion cameraAngle = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
        Vector3 screenMovementForward = cameraAngle * Vector3.forward;
        Vector3 screenMovementRight = cameraAngle * Vector3.right;

        float horizontal = pDirection.x;
        float vertical = pDirection.z ;

        Vector3 direction = (screenMovementForward * vertical) + (screenMovementRight * horizontal);

        base.Move(direction.normalized, pSpeed);
    }
}
