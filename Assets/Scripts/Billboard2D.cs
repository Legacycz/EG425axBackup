using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard2D : MonoBehaviour
{
    public Transform LookTarget;
    public Vector3 offsetVector = Vector3.right;

    void LateUpdate()
    {
        transform.LookAt(transform.position + Vector3.up);
        transform.Rotate(Vector3.right);
    }
}