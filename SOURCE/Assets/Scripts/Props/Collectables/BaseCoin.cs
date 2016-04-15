using UnityEngine;
using System.Collections;

public class BaseCoin : BaseCollectable
{
    protected override void OnCollect(BaseActor pCollector)
    {
        base.OnCollect(pCollector);

        InventoryManager.Instance.AddCoins(10);
    }
}
