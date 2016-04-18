using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    [Range(-10, 10)]
    private float zoom = 0;

    public float zoomSensitivity = 4.0f;

    public Vector3 targetFollowOffset;

    public Transform followTarget;
    public float followTargetSpeed = 100.0f;

    private void LateUpdate()
    {
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        transform.position = Vector3.MoveTowards(transform.position, followTarget.position + targetFollowOffset + (transform.forward * zoom ), Time.deltaTime * followTargetSpeed);
    }
}
