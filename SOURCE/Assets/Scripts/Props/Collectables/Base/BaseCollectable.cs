using UnityEngine;
using System.Collections;

public class BaseCollectable : MonoBehaviour, IPoolObject 
{
    private SpawnPool myPool;
    private SpawnPool onCollectFXPool;

    protected virtual SpawnPool OnCollectFXPool{
        get{
            return onCollectFXPool;
        }

        set{
            onCollectFXPool = value ;
        }
    }

    protected virtual void OnTriggerEnter(Collider pCollider)
    {
        BasePlayer player = pCollider.GetComponent<BasePlayer>();

        if(player != null)
        {
            OnCollect(player);
        }
    }

    protected virtual void OnCollect(BaseActor pCollector)
    {
        OnCollectFXPool.Spawn(transform.position, Quaternion.identity);

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
    }

    public void OnDespawn()
    {
    }

#endregion
}
