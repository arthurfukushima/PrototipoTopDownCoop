using UnityEngine;
using System.Collections;

public class Chest : BaseActor
{
    private SpawnPool goldCoinPool;

    private ShakeComponent shakeComponent;


    public SpawnPool _GoldCoinPool {
        get {
            if(goldCoinPool == null)
                goldCoinPool = PoolManager.Instance.GetPool("GoldCoin_Pool");

            return goldCoinPool;
        }
    }

    public ShakeComponent _ShakeComponent {
        get {
            if(shakeComponent == null)
                shakeComponent = GetComponent<ShakeComponent>();

            return shakeComponent;
        }
    }

    public override void ReceiveDamage(BaseActor pBully, int pDamage)
    {
        base.ReceiveDamage(pBully, pDamage);

        if(currentHealth > 0.0f)
        {
            _ShakeComponent.ShakeModel(0.25f, 2f, 0.05f);

            DropCoins(1);
        }
    }

    public override void OnZeroHealth()
    {
        base.OnZeroHealth();
        OnFinishedDeath();
    }

    public override void OnFinishedDeath()
    {
        base.OnFinishedDeath();

        DropCoins(Random.Range(1, 5));
        gameObject.SetActive(false);
    }

    private void DropCoins(int pAmount)
    {
        for (int i = 0; i < pAmount; i++)
        {
            Vector3 randomCircle = Random.insideUnitCircle;
            randomCircle.x *= Random.Range(1.0f, 2.5f);
            randomCircle.z = randomCircle.y;
            randomCircle.z *= Random.Range(1.0f, 2.5f);

            randomCircle.y = 0.0f;

            _GoldCoinPool.Spawn(transform.position + randomCircle, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
    }
}
