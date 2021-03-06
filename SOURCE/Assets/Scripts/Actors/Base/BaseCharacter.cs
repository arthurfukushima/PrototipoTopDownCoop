﻿using UnityEngine;
using System.Collections;

public class BaseCharacter : BaseActor
{
    private AnimationController animationController;
    private AnimationEventsController animationEventsController;

    private AttackController attackController;
    private PhysicsController physicsController;
    private GameplayController gameplayController;

    protected SKStateMachine<BaseCharacter> stateMachine;

    public AnimationController _AnimationController 
    {
        get {
            if(animationController == null)
                animationController = GetComponent<AnimationController>();
            
            return animationController;
        }
    }

    public PhysicsController _PhysicsController {
        get {
            if(physicsController == null)
                physicsController = GetComponent<PhysicsController>();

            return physicsController;
        }
    }

    public GameplayController _GameplayController {
        get {
            if(gameplayController == null)
                gameplayController = GetComponent<GameplayController>();

            return gameplayController;
        }
    }

    public AnimationEventsController _AnimationEventsController {
        get {
            if(animationEventsController == null)
                animationEventsController = GetComponent<AnimationEventsController>();

            return animationEventsController;
        }
    }

    public AttackController _AttackController 
    {
        get {
            if(attackController == null)
                attackController = GetComponent<AttackController>();

            return attackController;
        }
    }

    public virtual SKStateMachine<BaseCharacter> _StateMachine 
    {
        get {
            if(stateMachine == null)
            {
                stateMachine = new SKStateMachine<BaseCharacter>(this, new BaseIdleState());
                stateMachine.AddState(new BaseRunState());
                stateMachine.AddState(new BaseAttackState());
                stateMachine.AddState(new BaseRollState());
                stateMachine.AddState(new BaseDamageState());
            }

            return stateMachine;
        }
    }
   
#region MONO
    protected virtual void Update()
    {
        _StateMachine.Update(Time.deltaTime);
    }
#endregion

    public override void OnZeroHealth()
    {
        base.OnZeroHealth();

        ChangeState<BaseDeathState>();
    }

    public override void OnFinishedDeath()
    {
        base.OnFinishedDeath();
    }

#region MOVEMENT
    public virtual void Move(Vector3 pDirection, float pSpeed)
    {
        Vector3 velocity = pDirection * pSpeed;
        velocity.y = _PhysicsController.Velocity.y;

        _PhysicsController.Velocity = velocity;
    }

    public void UpdateRotation()
    {
        Vector3 hitPosition = RaycastMousePosition();
        hitPosition.y = transform.position.y;

        Vector3 rightAxis = new Vector3(-Input.GetAxisRaw("JoyRight_X"), 0, Input.GetAxisRaw("JoyRight_Y"));

        if(rightAxis != Vector3.zero)
            hitPosition = transform.position + rightAxis;
        else
        {
            Vector3 velocity = _PhysicsController.Velocity.normalized;
            velocity.y = 0.0f;

            hitPosition = transform.position - velocity;
        }

        Vector3 targetLook = transform.position - hitPosition;

        if(targetLook != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(targetLook);
    }

    public void Rotate(Vector3 pDirection, float pSpeed)
    {
        if(pDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(pDirection);
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, rotation, Time.deltaTime * pSpeed);
        }
    }
#endregion

#region STATE MACHINE
    public void ChangeState<R>() where R : SKState<BaseCharacter>
    {
        _StateMachine.ChangeState<R>();
    }
#endregion

    public virtual void Knockback(Vector3 pDirection, float pForce)
    {
        
    }

    public Vector3 RaycastMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, _PhysicsController.groundLayers))
        {
//            Debug.DrawLine(ray.origin, hit.point, Color.red);
            return hit.point;
        }

        return Vector3.zero;
    }
}
