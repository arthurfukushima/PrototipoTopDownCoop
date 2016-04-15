using UnityEngine;
using System.Collections;

public class BaseActor : MonoBehaviour 
{
    public int maxHealth;
    public int currentHealth;

    public bool _IsAlive{
        get{
            return currentHealth > 0.0f;
        }
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void ApplyDamageTo(BaseActor pActor, int pDamage)
    {
        if(pActor != null)
            pActor.ReceiveDamage(pDamage);
    }

    public virtual void ReceiveDamage(int pDamage)
    {
        if(!_IsAlive)
            return;

        currentHealth -= pDamage;

        if(currentHealth <= 0.0f)
        {
            OnZeroHealth();    
        }
    }

    public virtual void OnZeroHealth()
    {
        
    }

    public virtual void OnDeath()
    {
        
    }
}
