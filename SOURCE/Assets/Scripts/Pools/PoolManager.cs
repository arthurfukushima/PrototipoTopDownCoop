using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPoolObject
{
	void OnSpawn(SpawnPool pMyPool);
	void Despawn();
	void DespawnIn(float fDelay);
	void OnDespawn();
	 
	/*
	public void OnSpawn(SpawnPool pMyPool)
	{
	}

	public void Despawn()
	{
	}

	public void DespawnIn(float pDelay)
	{
	}

	public void OnDespawn()
	{
	}
	*/
}

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
	public Dictionary<string, SpawnPool> spawnPoolsDictionary;
	
	private static GameObject m_poolsParent;

	public static GameObject PoolsParent 
	{
		get {
			if(m_poolsParent == null)
				m_poolsParent = Instance.gameObject;

			return m_poolsParent;
		}
	}

    protected override bool DestroyOnLoad
    {
        get{return true;}
    }
	
	public void RegisterPool(string sName, SpawnPool pPool)
	{
		if(spawnPoolsDictionary == null)
			spawnPoolsDictionary = new Dictionary<string, SpawnPool>();
		
		if(!spawnPoolsDictionary.ContainsKey(sName))
			spawnPoolsDictionary.Add(sName, pPool);
	}
	
	public SpawnPool GetPool(string sName)
	{
		if(spawnPoolsDictionary == null)
		{
			spawnPoolsDictionary = new Dictionary<string, SpawnPool>();
			return null;
		}
		else if(spawnPoolsDictionary.ContainsKey(sName))
			return spawnPoolsDictionary[sName];
		else
			return null;
	}
	
	public static SpawnPool CreatePool(GameObject pPrefab, string pName, int pInstances)
    {
        if(pPrefab != null)
        {
            GameObject newPool = new GameObject(pName);
            SpawnPool pool = newPool.AddComponent<SpawnPool>();
            pool.Initialize(pPrefab, pName, pInstances);
            newPool.transform.SetParent(PoolsParent.transform);

            return pool;
        }
        else
        {
            Debug.LogError("Could not create pool - " + pName + " because the prefab is null.");
            return null;
        }
    }

}