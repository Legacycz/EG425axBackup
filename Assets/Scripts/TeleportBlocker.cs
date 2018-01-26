using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportBlocker : MonoBehaviour {
    public VRTK_InteractTouch thisTouch;
    public VRTK_BasePointerRenderer thisPointer;

    private List<GameObject> touchedWalls = new List<GameObject>();

    private void Start()
    {
        thisTouch.ControllerStartUntouchInteractableObject += ThisTouch_ControllerStartUntouchInteractableObject;
        thisTouch.ControllerStartTouchInteractableObject += ThisTouch_ControllerStartTouchInteractableObject;
    }

    private void ThisTouch_ControllerStartTouchInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        if (e.target.layer == LayerMask.NameToLayer("Wall"))
        {
            thisPointer.enabled = false;
            touchedWalls.Add(e.target);
        }
    }

    private void ThisTouch_ControllerStartUntouchInteractableObject(object sender, ObjectInteractEventArgs e)
    {
        if (e.target.layer == LayerMask.NameToLayer("Wall"))
        {
            touchedWalls.Remove(e.target);
            if (touchedWalls.Count == 0)
            {
                thisPointer.enabled = true;
            }
        }
    }
}
