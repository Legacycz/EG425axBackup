﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorDoor : MonoBehaviour {

    public bool Open = false;
    public float TimeToOpen = 1;
    public GameObject Doors;
    public Vector3 Direction = Vector3.up;
    public float Height = 10;
    private bool _opening;
    private float _openedHight = 0;


    [ContextMenu("Click")]
    void OnMouseDown()
    {
        if(!_opening)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        _opening = true;
        float start = Height;
        float end = 0;
        float direction = -1;
        if(!Open)
        {
             start = 0;
             end = Height;
            direction = 1;
        }
        float time = Time.time;
        float timeEnd = time + TimeToOpen;
        Vector3 startPosition = Doors.transform.position;
        Vector3 endPosition = startPosition + direction * Direction * Height;
        while(Time.time - time <= TimeToOpen)
        {
            Vector3 pos =  Vector3.Lerp(startPosition, endPosition, (Time.time - time) / TimeToOpen);
            Doors.transform.position = pos;
            yield return null;
        }
        Doors.transform.position = endPosition;        
        Open = !Open;
        _opening = false;

    }
}
