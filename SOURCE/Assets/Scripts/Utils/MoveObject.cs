using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour 
{
    public float movementSpeed = 5.0f;

    public void MoveTo(Vector3 pWorldPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, pWorldPosition, Time.deltaTime * movementSpeed);
    }
}
