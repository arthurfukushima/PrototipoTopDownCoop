using UnityEngine;
using System.Collections;

public class EnableSkeletons : MonoBehaviour 
{
    private void OnTriggerEnter(Collider pCollider)
    {
        BasePlayer player = pCollider.GetComponent<BasePlayer>();

        if(player != null)
        {
            foreach(Skeleton skeleton in GetComponentsInChildren<Skeleton>())
            {
                skeleton.enabled = true;
            }
        }
    }
}
