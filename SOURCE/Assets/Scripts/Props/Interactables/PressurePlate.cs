using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour 
{
    private Vector3 originalPosition;
    private List<Rigidbody> pressingRigidbodies = new List<Rigidbody>();

    public Vector3 pressedPositionOffset;
    public bool isPressed;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider pCollider)
    {
        if(!isPressed)
        {
            if(pCollider.attachedRigidbody != null)
            {
                isPressed = true;

                if(!pressingRigidbodies.Contains(pCollider.attachedRigidbody))
                    pressingRigidbodies.Add(pCollider.attachedRigidbody);
            }
        }
    }

    private void OnTriggerExit(Collider pCollider)
    {
        if(isPressed)
        {
            if(pCollider.attachedRigidbody != null)
            {
                if(pressingRigidbodies.Contains(pCollider.attachedRigidbody))
                    pressingRigidbodies.Remove(pCollider.attachedRigidbody);

                if(pressingRigidbodies.Count == 0)
                    isPressed = false;
            }
        }
    }

    private void Update()
    {
        if(isPressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition - pressedPositionOffset, Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime);
        }
    }
}
