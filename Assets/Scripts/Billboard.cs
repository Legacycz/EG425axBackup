using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform LookTarget;
    public Vector3 offsetVector = Vector3.right;

    void LateUpdate()
    {
        transform.eulerAngles = LookTarget.transform.eulerAngles + offsetVector * 180;
    }
}