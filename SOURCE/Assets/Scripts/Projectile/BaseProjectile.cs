using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BaseProjectile : MonoBehaviour, IPoolObject
{
    private SpawnPool myPool;
    private PhysicsController physicsController;

    private BaseActor owner;
    private int damage;

    private List<BaseActor> targetsHitted = new List<BaseActor>();

    public PhysicsController _PhysicsController {
        get {
            if(physicsController == null)
                physicsController = GetComponent<PhysicsController>();

            return physicsController;
        }
    }

    public void Create(BaseActor pOwner, Vector3 pDirection, float pMovementSpeed, int pDamage)
    {
        owner = pOwner;
        damage = pDamage;
        _PhysicsController.AddForce(pDirection * pMovementSpeed);
    }

    private void OnTriggerEnter(Collider pCollider)
    {
        BaseActor actor = pCollider.GetComponent<BaseActor>();

        if(actor != null)
        {
            if(!targetsHitted.Contains(actor))
            {
                targetsHitted.Add(actor);   
                owner.ApplyDamageTo(actor, damage);
            }
        }
    }

#region IPoolObject implementation

    public void OnSpawn(SpawnPool pMyPool)
    {
        myPool = pMyPool;

        DespawnIn(5.0f);
    }

    public void Despawn()
    {
        _PhysicsController.Velocity = Vector3.zero;
        _PhysicsController.CachedRigidbody.angularVelocity = Vector3.zero;
    }

    public void DespawnIn(float fDelay)
    {
        myPool.DespawnIn(gameObject, fDelay);
    }

    public void OnDespawn()
    {
        
    }

#endregion
}
