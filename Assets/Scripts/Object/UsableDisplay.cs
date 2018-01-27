using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableDisplay : MonoBehaviour {
    public Text MainLabel;
    public Text[] ButtonLabels;
    public Button[] Buttons;

    public UsableBase Selected
    {
        get { return _selected; }
        set {
            _selected = value;
            UpdateView();
        }
    }

    public void OnEnable()
    {
        Buttons[0].onClick.AddListener(PressButton0);
        Buttons[1].onClick.AddListener(PressButton1);
        Buttons[2].onClick.AddListener(PressButton2);
        Buttons[3].onClick.AddListener(PressButton3);
    }

    public void OnDisable()
    {
        Buttons[0].onClick.RemoveListener(PressButton0);
        Buttons[1].onClick.RemoveListener(PressButton1);
        Buttons[2].onClick.RemoveListener(PressButton2);
        Buttons[3].onClick.RemoveListener(PressButton3);
    }

    void PressButton0()
    {
        if(_selected)
        {
            _selected.Buttons[0].Action.Invoke();
        }
    }

    void PressButton1()
    {
        if (_selected)
        {
            _selected.Buttons[0].Action.Invoke();
        }
    }

    void PressButton2()
    {
        if (_selected)
        {
            _selected.Buttons[0].Action.Invoke();
        }
    }

    void PressButton3()
    {
        if (_selected)
        {
            _selected.Buttons[0].Action.Invoke();
        }
    }

    public void UpdateView()
    {
        if(_selected)
        {
            gameObject.SetActive(true);
            MainLabel.text = _selected.Desctription;

            for(int i = 0; i < 4;i++)
            {
                if(i < _selected.Buttons.Length)
                {
                    Buttons[i].gameObject.SetActive(true);
                    Buttons[i].interactable = _selected.Buttons[i].Active;
                    ButtonLabels[i].text = _selected.Buttons[i].Label;
                    Image img = Buttons[i].GetComponent<Image>();
                    if(img)
                    {
                        img.color = _selected.Buttons[i].Color;
                    }
                }
                else
                {
                    Buttons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private UsableBase _selected;


	
}
