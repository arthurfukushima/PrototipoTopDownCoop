using UnityEngine;
using System.Collections;

public class BaseSpecialSkill : SKState<BaseCharacter>
{
    public override void Begin()
    {
        base.Begin();

        Collider[] colliders = Physics.OverlapSphere(context.transform.position, 3.0f);

        foreach(Collider col in colliders)
        {
            BaseAI ai = col.GetComponent<BaseAI>();

            if(ai != null)
            {
                Vector3 dir = (ai.transform.position - context.transform.position).normalized;
                ai.Knockback(dir, 5.0f);
            }
        }
    }

    public override void Reason()
    {
        base.Reason();

        if(timeOnState > 0.5f)
        {
            context.ChangeState<BaseIdleState>();
        }
    }
}
