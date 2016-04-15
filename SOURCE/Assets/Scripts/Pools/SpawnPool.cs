using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPool : MonoBehaviour 
{
	public string m_poolName;					//The pool name (important to find them later)
	public GameObject m_objectPrefab;			//The pool object
	
	public bool m_createOnStart;                //Will create some instances on start?
    public bool m_adoptChildren;                //If there is any GO, adopt it as a Pool Object.

	public int m_startInstances;				//Starting instances.
	public int m_extraInstances = 1;  			//If we need more objects than we have created, create more X instances.
	
	protected List<GameObject> m_poolObjects;			//Objects in the pool
    protected List<GameObject> m_spawnedObjects;		//Spawned objects
	
    protected virtual string ObjectName{
        get{
            return m_objectPrefab.name;
        }
    }

	void Awake()
	{
		if(m_poolName == "")
			m_poolName = gameObject.name;

        if (m_poolObjects == null)
            m_poolObjects = new List<GameObject>();

        if (m_spawnedObjects == null)
            m_spawnedObjects = new List<GameObject>();

        if (m_adoptChildren)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    m_spawnedObjects.Add(child.gameObject);

					Debug.Log("ADOPT: " + child.name + " : " + child.position);
					//Debug.Log(child.transform.position, child.gameObject);

                    foreach(IPoolObject poolObj in child.GetComponents<IPoolObject>())
                        poolObj.OnSpawn(this);
                }
                else
                {
                    if (!m_poolObjects.Contains(child.gameObject))
                        m_poolObjects.Add(child.gameObject);
                }
            }
        }

        if (m_createOnStart)
			CreateInstances(m_startInstances);
	}
	
	public virtual void Initialize(GameObject pPrefab, string pPoolName, int pStartInstances)
	{
		m_objectPrefab = pPrefab;
		m_poolName = pPoolName;
		m_startInstances = pStartInstances;
		
		CreateInstances(m_startInstances);
	}
	
	/// <summary>
	/// Instantiate the objects 
	/// </summary>
	/// <param name='fCount'>
	/// The number of new instances.
	/// </param>
	public void CreateInstances(float fCount)
	{
		if(m_spawnedObjects == null)
			m_spawnedObjects = new List<GameObject>();

	    if (fCount <= 0)
	        fCount = 1;
		
		PoolManager.Instance.RegisterPool(m_poolName, this);
		
		for(int i = 0; i < fCount; i++)
		{
            InitializeGameObject(i);
		}
	}

    protected virtual void InitializeGameObject(int index){
        GameObject newInstance = InstantiateGameObject();
        newInstance.transform.SetParent(transform);
        newInstance.transform.localPosition = Vector3.zero;
        newInstance.SetActive(false);

        m_poolObjects.Add(newInstance);
    }

    protected virtual GameObject InstantiateGameObject(){
        return Instantiate(m_objectPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    }
	
	/// <summary>
	/// Search for an object in the pool and spawn it! Returns a GameObject!
	/// </summary>
	/// <param name='pPosition'>
	/// The position where the object will be created.
	/// </param>
	/// <param name='rRotation'>
	/// The rotation of the spawned object.
	/// </param>
	public virtual GameObject Spawn(Vector3 pPosition, Quaternion pRotation)
	{	
		if(m_poolObjects.Count == 0)
			CreateInstances(m_extraInstances);

	    if (m_poolObjects != null && m_poolObjects.Count > 0)
        {
            GameObject go = SpawnGivenObject(m_poolObjects[0], pPosition, pRotation);
            return go;
	    }

		Debug.Log("No more instances in the pool.");
		return null;
	}

	/// <summary>
	/// Search for an object in the pool and spawn it! Returns a Generic!
	/// </summary>
	/// <param name='pPosition'>
	/// The position where the object will be created.
	/// </param>
	/// <param name='rRotation'>
	/// The rotation of the spawned object.
	/// </param>
    public virtual T Spawn<T>(Vector3 pPosition, Quaternion pRotation) where T : Component
	{
		if(m_poolObjects.Count == 0)
			CreateInstances(m_extraInstances);

	    if (m_poolObjects != null && m_poolObjects.Count > 0)
	    {
            GameObject go = SpawnGivenObject(m_poolObjects[0], pPosition, pRotation);
            T instantiated = go.GetComponent<T>();
	        return instantiated;
	    }

        Debug.Log("No more instances of " + ObjectName + " in the pool.");
        return null;
    }

    /// <summary>
    /// Spawn the informed GameObject
    /// </summary>
    protected virtual GameObject SpawnGivenObject(GameObject givenObject, Vector3 pPosition, Quaternion pRotation){
        m_poolObjects.Remove(givenObject);

        givenObject.transform.position = pPosition;
        givenObject.transform.rotation = pRotation;
        givenObject.SetActive(true);

        foreach(IPoolObject poolObj in givenObject.GetComponents<IPoolObject>())
            poolObj.OnSpawn(this);

        m_spawnedObjects.Add(givenObject);

        return givenObject;
    }
	
	/// <summary>
	/// Disable the object and put it back in the pool.
	/// </summary>
	/// <param name='pPoolObject'>
	/// The object that will be despawned.
	/// </param>
	public void Despawn(GameObject pPoolObject)
	{
	    if (m_spawnedObjects.Contains(pPoolObject))
	    {
	        m_spawnedObjects.Remove(pPoolObject);
            m_poolObjects.Insert(0, pPoolObject);

            foreach(IPoolObject poolObj in pPoolObject.GetComponents<IPoolObject>())
                poolObj.OnSpawn(this);

			pPoolObject.transform.SetParent(transform);
            pPoolObject.SetActive(false);
	    }
	}
	
	private IEnumerator _Despawn(GameObject pPoolObject, float pTime)
	{
		yield return new WaitForSeconds(pTime);
		
		if(pPoolObject.activeSelf)
			Despawn(pPoolObject);
	}
	
	/// <summary>
	/// Despawn an object in X seconds
	/// </summary>
	/// <param name='pPoolObject'>
	/// The pool object.
	/// </param>
	/// <param name='pTime'>
	/// Time in seconds.
	/// </param>
	public void DespawnIn(GameObject pPoolObject, float pTime)
	{
		StartCoroutine(_Despawn(pPoolObject, pTime));
	}
	
	/// <summary>
	/// Disable the first object in the pool.
	/// </summary>
	public void DespawnFirst()
	{
        if(m_spawnedObjects != null && m_spawnedObjects.Count > 0)
            Despawn(m_spawnedObjects[0]);
	}
	
	/// <summary>
	/// Disable all the object in the pool.
	/// </summary>
	public void DespawnAll()
	{
		if(m_spawnedObjects != null)
		{
			while(m_spawnedObjects.Count!= 0)
				Despawn( m_spawnedObjects[0]);
		}
	}

	/// <summary>
	/// Return a list of spawned objects.
	/// </summary>
	/// <returns>The spawned objects.</returns>
	public List<GameObject> GetSpawnedObjects()
	{
		if(m_spawnedObjects == null)
			m_spawnedObjects = new List<GameObject>();

		return m_spawnedObjects;
	}
}
