﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorDoor : UsableBase {

    public bool Open = false;
    public float TimeToOpen = 1;
    public GameObject Doors;
    public Vector3 Direction = Vector3.up;
    public float Height = 10;
    public SpriteRenderer doorIcon;
    public Sprite openedSprite;
    public Sprite closedSprite;
    public RunePuzzle connectedPuzzle;

    private bool _opening;

    /* [ContextMenu("Click")]
     void OnMouseDown()
     {
         print("door opened");
         if(!_opening)
         {
             StartCoroutine(OpenDoor());
         }
     }*/

    /*void OpenDoor()
    {
        if (!_opening)
        {
            StartCoroutine(MoveDoor());
        }
    }*/

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (connectedPuzzle)
        {
            RedrawPasswordView();
        }
    }

    [ContextMenu("RedrawPassword")]
    public void RedrawPasswordView()
    {
        StartCoroutine(DelayedRedrawPasswordView());
    }

    private IEnumerator DelayedRedrawPasswordView()
    {
        yield return null;
        for (int i = 0; i < MazeLevelManager.Instance.runePuzzleSolutionGUI.solutionRunes.Length; ++i)
        {
            MazeLevelManager.Instance.runePuzzleSolutionGUI.solutionRunes[i].sprite = connectedPuzzle.solution[i].spriteRenderer.sprite;
        }
    }

    public void SetPuzzleSolved (bool newSolved)
    {
        connectedPuzzle.isSolved = newSolved;
    }

    public void OpenDoor()
    {
        if (connectedPuzzle)
        {
            if (connectedPuzzle.isSolved) // if there is some connected puzzle and it's also solved, then you can open the door.
            {
                if (!_opening)
                {
                    StartCoroutine(MoveDoor());
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        Buttons[i].Active = false;
                    }
                    MazeLevelManager.Instance.Usable.UpdateView();
                }
            }
        }
        else
        {
            if (!_opening)
            {
                StartCoroutine(MoveDoor());
                for (int i = 0; i < Buttons.Length; i++)
                {
                    Buttons[i].Active = false;
                }
                MazeLevelManager.Instance.Usable.UpdateView();
            }
        }
    }

    IEnumerator MoveDoor()
    {
        GetComponent<AudioSource>().Play();
        _opening = true;
        if (!Open)
            doorIcon.sprite = openedSprite;
        else
            doorIcon.sprite = closedSprite;
        float direction = -1;
        if (!Open)
        {
            direction = 1;
        }
        float time = Time.time;
        Vector3 startPosition = Doors.transform.position;
        Vector3 endPosition = startPosition + direction * Direction * Height;
        while (Time.time - time <= TimeToOpen)
        {
            Vector3 pos = Vector3.Lerp(startPosition, endPosition, (Time.time - time) / TimeToOpen);
            Doors.transform.position = pos;
            yield return null;
        }
        Doors.transform.position = endPosition;
        Open = !Open;
        _opening = false;
        if (Buttons.Length >= 2)
        {
            Buttons[0].Active = !Open;
            Buttons[1].Active = Open;
        }
        MazeLevelManager.Instance.Usable.UpdateView();
    }
}
