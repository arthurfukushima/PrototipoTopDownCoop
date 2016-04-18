using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour 
{
    public GameObject knightPrefab;
    public GameObject witchPrefab;

    public Transform knightPosition;
    public Transform witchPosition;

    private GameObject knightGO;
    private GameObject witchGO;

    void OnTriggerEnter(Collider pCollider)
    {
        BasePlayer player = pCollider.GetComponent<BasePlayer>();

        if(player != null)
        {
            knightGO = Instantiate(knightPrefab, knightPosition.position, Quaternion.identity) as GameObject;
            witchGO = Instantiate(witchPrefab, witchPosition.position, Quaternion.identity) as GameObject;

            GetComponent<Collider>().enabled = false;
        }
    }
}
