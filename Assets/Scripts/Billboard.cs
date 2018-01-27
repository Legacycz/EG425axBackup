using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //public Transform LookTarget;
    public Vector3 offsetVector = Vector3.right;

    void LateUpdate()
    {
        if (MazeLevelManager.Instance.vrPlayer)
        {
            transform.eulerAngles = MazeLevelManager.Instance.vrPlayer.transform.eulerAngles + offsetVector * 180;
            Vector3 tmp = transform.localEulerAngles;
            tmp.z = 0;
            transform.localEulerAngles = tmp;
        }
    }
}