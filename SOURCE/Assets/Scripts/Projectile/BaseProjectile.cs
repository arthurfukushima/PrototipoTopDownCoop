using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseProjectile : MonoBehaviour 
{
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
}
