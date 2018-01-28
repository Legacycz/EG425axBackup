using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void ActionButton();

[Serializable]
public struct ButtonInfo
{
    public string Label;
    public bool Active;
    public Color Color;

    public UnityEvent Action;
}

public class UsableBase : MonoBehaviour {

    public string Desctription = "";

    public ButtonInfo[] Buttons;

    private Transform _parent;

    void OnEnable()
    {
        _parent = transform.parent;
        transform.parent = null;
    }

    

    public virtual void OnMouseDown()
    {
       if( MazeLevelManager.Instance.Usable.Selected != this)
       {
            MazeLevelManager.Instance.Usable.Selected = this;
       }
    }


}
