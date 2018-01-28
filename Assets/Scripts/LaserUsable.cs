using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUsable : UsableBase {

    public LaserArea Controller;
    public Color ColorActivate;
    public Color ColorDeactivate;
    public string ActivateText;
    public string DeactivateText;
    public SpriteRenderer Icon1;

    public void Start()
    {
        if(Controller)
        {
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
       
            if (Controller.LasersVisual.activeInHierarchy)
            {
                if (Buttons.Length > 0)
                {
                    Buttons[0].Color = ColorDeactivate;
                    Buttons[0].Label = DeactivateText;
                }
                if (Icon1 != null)
                {
                    Icon1.color = ColorActivate;
                }
            }
            else
            {
            if (Buttons.Length > 0)
            {
                Buttons[0].Color = ColorActivate;
                Buttons[0].Label = ActivateText;
            }
                if(Icon1 != null)
                {
                    Icon1.color = ColorDeactivate;
                }
            }
        }
    

    public void UseLaser()
    {
        Controller.ToggleLaserArea();
        UpdateButton();
        MazeLevelManager.Instance.Usable.UpdateView();
    }
}