using System;
using UnityEngine;
using System.Collections;

public class ParticlePoolObject : MonoBehaviour, IPoolObject
{
	private SpawnPool m_myPool;

	public ParticleSystem m_particleSystem;

	public bool m_autoDespawn;			// If the particle will call the despawn function afted X seconds.
	public float overLifeTime;			// Overlife to despawn after the particle has finished.

	public ParticleSystem ParticleSystem {
		get {
			if(m_particleSystem == null)
				m_particleSystem = GetComponentInChildren<ParticleSystem>();

			return m_particleSystem;
		}
	}
    
	private IEnumerator DespawnInCoroutine(float fSeconds)
	{
		if(ParticleSystem != null)
		{
			yield return new WaitForSeconds(ParticleSystem.duration);
			ParticleSystem.Stop(true);
			yield return new WaitForSeconds(ParticleSystem.duration);
		}

		yield return new WaitForSeconds(overLifeTime + fSeconds);

		Despawn();
	}

	public void OnSpawn(SpawnPool pMyPool)
	{
		m_myPool = pMyPool;

		if(m_autoDespawn)
			StartCoroutine(DespawnInCoroutine(overLifeTime));
	}

	public void DespawnIn(float fSeconds)
	{
		if(gameObject.activeSelf)
			StartCoroutine(DespawnInCoroutine(fSeconds));
	}

	public void DespawnImmediately()
	{
		if(m_particleSystem != null)
			m_particleSystem.Stop();

		Despawn();
	}

	public void StopAndDespawn(float pDelay = 0.0f)
	{
		StartCoroutine(DespawnInCoroutine(pDelay));
	}

	public void Despawn()
	{
		if(m_myPool != null)
			m_myPool.Despawn(gameObject);
		else
			Debug.LogError("Can't Despawn " + gameObject.name + " because the Pool is null");
	}

	public void OnDespawn()
	{
	}
}
