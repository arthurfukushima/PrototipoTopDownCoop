using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

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

        _PhysicsController.Velocity = Vector3.zero;
        _PhysicsController.CachedRigidbody.angularVelocity = Vector3.zero;
        _PhysicsController.AddForce(pDirection * pMovementSpeed);

        StopAllCoroutines();
        StartCoroutine(DespawnCoroutine(4.0f));
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

    private IEnumerator DespawnCoroutine(float pDelay)
    {
        yield return new WaitForSeconds(pDelay);
        Despawn();
    }

#region IPoolObject implementation

    public void OnSpawn(SpawnPool pMyPool)
    {
        myPool = pMyPool;
    }

    public void Despawn()
    {
        myPool.Despawn(gameObject);
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
