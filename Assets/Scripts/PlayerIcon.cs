using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {
    public VRPlayer playerParent;

    private void Start()
    {
        if(!playerParent.enabled)
        {
            transform.SetParent(null);
        }
    }

    void OnMouseDown()
    {
        playerParent.EnablePlayer();
    }
}
