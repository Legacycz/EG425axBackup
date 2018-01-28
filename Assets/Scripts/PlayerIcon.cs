using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : UsableBase {
    public VRPlayer playerParent;
    public string destcriptionEmpty= "Use it";
    public string destcriptioFull = "Your freinds";

    private void Start()
    {
        if(!playerParent.enabled)
        {
            transform.SetParent(null);
            SetReadyToActivate();

        }
        else
        {
            Desctription = destcriptioFull;
            Buttons[0].Active = false;
        }
       
    }

    public void SetReadyToActivate()
    {
        Buttons[0].Active = true;
        Desctription = destcriptionEmpty;
    }

    public void Activete()
    {

        playerParent.enabled = true;
        MazeLevelManager.Instance.Usable.Selected = null;
    }
}
