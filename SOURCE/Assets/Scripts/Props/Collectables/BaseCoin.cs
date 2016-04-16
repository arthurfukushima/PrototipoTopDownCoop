using UnityEngine;
using System.Collections;

public class BaseCoin : BaseCollectable
{
    protected override SpawnPool OnCollectFXPool {
        get {
            if(base.OnCollectFXPool == null)
                OnCollectFXPool = PoolManager.Instance.GetPool("CoinCollect_FX_Pool");

            return base.OnCollectFXPool;
        }
    }

    protected override void OnCollect(BaseActor pCollector)
    {
        base.OnCollect(pCollector);

        InventoryManager.Instance.AddCoins(10);
    }
}
