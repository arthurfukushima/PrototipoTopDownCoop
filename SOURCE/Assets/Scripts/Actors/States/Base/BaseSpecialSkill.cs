using UnityEngine;

public class BaseSpecialSkill : SKState<BaseCharacter>
{
    private SpawnPool specialSkillFXPool;

    public SpawnPool _SpecialSkillFXPool {
        get {
            if(specialSkillFXPool == null)
                specialSkillFXPool = PoolManager.Instance.GetPool("SpecialAura_FX_Pool");   

            return specialSkillFXPool;
        }
    }

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

        _SpecialSkillFXPool.Spawn(context.transform.position + Vector3.up * 0.3f, Quaternion.Euler(90, 0, 0));
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
