using UnityEngine;
using System.Collections;

public class BaseTree : BaseActor
{
    private PhysicsController physicsController;

    private SpawnPool logPool;
    private ShakeComponent shakeComponent;

    public PhysicsController _PhysicsController {
        get {
            if(physicsController == null)
                physicsController = GetComponent<PhysicsController>();

            return physicsController;
        }
    }

    public SpawnPool _LogPool {
        get {
            if(logPool == null)
                logPool = PoolManager.Instance.GetPool("Log_Pool");

            return logPool;
        }
    }

    public ShakeComponent _ShakeComponent {
        get {
            if(shakeComponent == null)
                shakeComponent = GetComponent<ShakeComponent>();

            return shakeComponent;
        }
    }

    public override void ReceiveDamage(int pDamage)
    {
        base.ReceiveDamage(pDamage);

        if(currentHealth > 0.0f && _IsAlive)
        {
            _ShakeComponent.ShakeModel(0.25f, 2f, 0.05f);

            DropLogs(1);
        }
    }

    public override void OnZeroHealth()
    {
        base.OnZeroHealth();
        OnDeath();
    }

    public override void OnDeath()
    {
        base.OnDeath();

        DropLogs(Random.Range(1, 5));

        _PhysicsController.IsKinematic = false;
        _PhysicsController.UseGravity = true;
        _PhysicsController.CachedRigidbody.AddForceAtPosition(transform.forward * 50 * _PhysicsController.CachedRigidbody.mass, transform.position + Vector3.up * 3);

        StartCoroutine(DespawnCoroutine(5.0f));
    }

    private IEnumerator DespawnCoroutine(float pTime)
    {
        yield return new WaitForSeconds(pTime);

        gameObject.SetActive(false);
    }


    private void DropLogs(int pAmount)
    {
        for (int i = 0; i < pAmount; i++)
        {
            Vector3 randomCircle = Random.insideUnitCircle;
            randomCircle.x *= Random.Range(1.0f, 2.5f);
            randomCircle.z = randomCircle.y;
            randomCircle.z *= Random.Range(1.0f, 2.5f);

            randomCircle.y = 0.0f;

            _LogPool.Spawn(transform.position + randomCircle, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
    }
}
