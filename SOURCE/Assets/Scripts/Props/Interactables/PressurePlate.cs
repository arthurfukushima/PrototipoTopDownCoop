using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressurePlate : MonoBehaviour 
{
    private Vector3 originalPosition;
    private List<Rigidbody> pressingRigidbodies = new List<Rigidbody>();

    public Vector3 pressedPositionOffset;
    public bool isPressed;

    public Transform spike;
    private Vector3 spikeClosedPosition;

    private void Start()
    {
        originalPosition = transform.position;
        spikeClosedPosition = spike.position;
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
            spike.position = Vector3.MoveTowards(spike.position, spikeClosedPosition + Vector3.down * 1.5f, Time.deltaTime * 3.0f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime);
            spike.position = Vector3.MoveTowards(spike.position, spikeClosedPosition, Time.deltaTime * 3.0f);
        }
    }
}
