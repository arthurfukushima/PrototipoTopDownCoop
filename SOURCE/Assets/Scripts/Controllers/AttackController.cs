using UnityEngine;
using System.Collections;

public class AttackController : MonoBehaviour 
{
    public void Attack()    
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
}
