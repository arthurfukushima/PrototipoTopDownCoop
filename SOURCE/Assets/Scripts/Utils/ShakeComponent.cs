using UnityEngine;
using System.Collections;

public class ShakeComponent : MonoBehaviour 
{
	internal bool isShaking;
	internal Vector3 shakeNoise;

	public Vector3 shakingAxis = Vector3.one;

#region SHAKE
	public void Shake(float pDuration = 0.5f, float pSpeed = 4.0f, float pMagnitude = 0.2f)
	{
		StopAllCoroutines();
		StartCoroutine(ShakeCoroutine(pDuration, pSpeed, pMagnitude));
	}

	public void ShakeModel(float pDuration = 0.5f, float pSpeed = 4.0f, float pMagnitude = 0.2f)
	{
		StopAllCoroutines();
		StartCoroutine(ShakeModelCoroutine(pDuration, pSpeed, pMagnitude));
	}

	/// <summary>
	/// Produce a noise that can be added to camera's position, to simulate a shake.
	/// </summary>
	/// <returns>The coroutine.</returns>
	/// <param name="pDuration">P duration.</param>
	/// <param name="pSpeed">P speed.</param>
	/// <param name="pMagnitude">P magnitude.</param>
	private IEnumerator ShakeCoroutine(float pDuration, float pSpeed, float pMagnitude)
	{
		isShaking = true;
		float elapsed = 0.0f;
		
		//Vector3 originalCamPos = transform.position;
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		while (elapsed < pDuration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / pDuration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + pSpeed * percentComplete;

			shakeNoise = Vector3.zero;

			// map noise to [-1, 1]
			if(shakingAxis.x != 0.0f)
				shakeNoise.x = (Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f) - 1.0f;

			if(shakingAxis.y != 0.0f)
				shakeNoise.y = (Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f) - 1.0f;

			if(shakingAxis.z != 0.0f)
				shakeNoise.z = (Util.Noise.GetNoise(0.0f, 0.0f, alpha) * 2.0f) - 1.0f;

			shakeNoise.x *= pMagnitude * damper;
			shakeNoise.y *= pMagnitude * damper;
			shakeNoise.z *= pMagnitude * damper;

			shakeNoise = transform.TransformVector(shakeNoise);
			
			yield return null;
		}
		
		isShaking = false;
	}

	private IEnumerator ShakeModelCoroutine(float pDuration, float pSpeed, float pMagnitude)
	{
		Vector3 originalPosition = transform.position;

		isShaking = true;
		float elapsed = 0.0f;

		//Vector3 originalCamPos = transform.position;
		float randomStart = Random.Range(-1000.0f, 1000.0f);

		while (elapsed < pDuration) {

			elapsed += Time.deltaTime;			

			float percentComplete = elapsed / pDuration;			

			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);

			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + pSpeed * percentComplete;

			shakeNoise = Vector3.zero;

			// map noise to [-1, 1]
			if(shakingAxis.x != 0.0f)
				shakeNoise.x = (Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f) - 1.0f;

			if(shakingAxis.y != 0.0f)
				shakeNoise.y = (Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f) - 1.0f;

			if(shakingAxis.z != 0.0f)
				shakeNoise.z = (Util.Noise.GetNoise(0.0f, 0.0f, alpha) * 2.0f) - 1.0f;

			shakeNoise.x *= pMagnitude * damper;
			shakeNoise.y *= pMagnitude * damper;
			shakeNoise.z *= pMagnitude * damper;

			shakeNoise = transform.TransformVector(shakeNoise);

			transform.position = originalPosition + shakeNoise;

			yield return null;
		}

		isShaking = false;
	}
#endregion
}
