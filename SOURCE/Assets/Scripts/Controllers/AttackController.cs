using UnityEngine;

public class AttackController : MonoBehaviour 
{
    public SpawnPool iceBallProjectilePool;

    public SpawnPool _IceBallProjectilePool {
        get {
            if(iceBallProjectilePool == null)
                iceBallProjectilePool = PoolManager.Instance.GetPool("Projectile_IceBall_Pool");

            return iceBallProjectilePool;
        }
    }

    public void Attack(BaseActor pTarget, int pDamage)
    {
        
    }

    public void AttackAsSphere(BaseActor pAttacker, Vector3 pCenter, float pRadius, int pDamage)
    {
        Collider[] cols = Physics.OverlapSphere(pCenter, pRadius);

        foreach(Collider col in cols)
        {
            BaseActor actor = col.GetComponent<BaseActor>();

            if(actor != null)
            {
                if(actor != pAttacker)
                    pAttacker.ApplyDamageTo(actor, pDamage);  
            }
        }
    }

    public BaseProjectile AttackRanged(BaseActor pOwner, int pDamage, Vector3 pOrigin, Vector3 pDirection, float pSpeed)
    {
        BaseProjectile projectile = _IceBallProjectilePool.Spawn<BaseProjectile>(pOrigin, Quaternion.identity);
        projectile.Create(pOwner, pDirection, pSpeed, pDamage);

        return projectile;
    }
}
