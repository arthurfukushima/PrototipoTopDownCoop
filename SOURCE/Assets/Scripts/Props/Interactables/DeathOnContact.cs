using UnityEngine;
using System.Collections;

public class DeathOnContact : MonoBehaviour
{
    private void OnCollisionEnter(Collision pCollision)
    {
        BaseActor actor = pCollision.gameObject.GetComponent<BaseActor>();

        if(actor != null)
        {
            actor.ReceiveDamage(null, 200);
        }
    }
}
