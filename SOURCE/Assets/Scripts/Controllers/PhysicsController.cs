using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsController : MonoBehaviour 
{
	private Rigidbody cachedRigidbody;
	private Collider cachedCollider;
	private float velocity;

	internal Vector3 lastGrondedPosition;

	[Header("Ground Check")]
	public LayerMask groundLayers;
	public float skinWidth = 0.05f;

	[HideInInspector]
	public bool applyExtraGravity;
	[HideInInspector]
	public Vector3 extraGravity;
	[HideInInspector]
	public ForceMode gravityForceMode;


#region PROPERTIES
	public Rigidbody CachedRigidbody {
		get {
			if(cachedRigidbody == null)
				cachedRigidbody = GetComponent<Rigidbody>();

			return cachedRigidbody;
		}
	}

	public Collider CachedCollider {
		get {
			if(cachedCollider == null)
				cachedCollider = GetComponent<Collider>();

			return cachedCollider;
		}
	}
	
	public Vector3 Velocity {
		get {
			return CachedRigidbody.velocity;
		}
		set {
			CachedRigidbody.velocity = value;
		}
	}

	public bool IsKinematic{
		get{
			return CachedRigidbody.isKinematic;
		}
		set{
			CachedRigidbody.isKinematic = value;
		}
	}

	public bool UseGravity{
		get{
			return CachedRigidbody.useGravity;
		}
		set{
			CachedRigidbody.useGravity = value;
		}
	}

	public Vector3 ColliderSize{
		get{
			return CachedCollider.bounds.size;
		}
	}

	public Vector3 ColliderCenter{
		get{
			return CachedCollider.bounds.center;
		}
	}

	public Vector3 ExtraGravity {
		get {
			return extraGravity;
		}
		set {
			extraGravity = value;
		}
	}

	public ForceMode GravityForceMode {
		get {
			return gravityForceMode;
		}
		set {
			gravityForceMode = value;
		}
	}

#endregion

	protected void Awake()
	{
	}

	protected void FixedUpdate()
	{
		if(applyExtraGravity)
		{
			AddForce(extraGravity, gravityForceMode);
		}
	}

	public void AddForce(Vector3 pForce, ForceMode pMode = ForceMode.VelocityChange)
	{
		CachedRigidbody.AddForce(pForce, pMode);
	}

	public void AddRelativeForce(Vector3 pForce, ForceMode pMode = ForceMode.VelocityChange)
	{
		CachedRigidbody.AddRelativeForce(pForce, pMode);
	}

	public virtual bool IsGrounded()
	{
		Vector3 position = transform.position;

		//Check the floor a little earlier, to avoid collisions before is grounded.
		position.y += (skinWidth * 0.8f);	

        if(Physics.CheckSphere(position, skinWidth, groundLayers.value))
		{
			lastGrondedPosition = transform.position;
			return true;
		}

		return false;
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Vector3 position = transform.position;
		position.y += skinWidth;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(position, skinWidth);
	}
#endif
}
