using UnityEngine;
using System.Collections;

public class BasePlayer : BaseCharacter
{
    public bool hasControl;

    protected override void Start()
    {
        base.Start();

        PlayersControlManager.Instance.RegisterPlayer(this);
    }

    protected override void Update()
    {
        if(hasControl)
            _StateMachine.Update(Time.deltaTime);
    }
}
