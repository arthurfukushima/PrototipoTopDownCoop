using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    public Vector3 targetFollowOffset;

    public Transform followTarget;
    public float followTargetSpeed = 100.0f;

    private void Start()
    {
//        targetFollowOffset = transform.position - followTarget.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, followTarget.position + targetFollowOffset, Time.deltaTime * followTargetSpeed);
    }
}
