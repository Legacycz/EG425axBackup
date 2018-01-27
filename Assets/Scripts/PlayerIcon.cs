using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {
    public VRPlayer playerParent;

    void OnMouseDown()
    {
        playerParent.EnablePlayer();
    }
}
