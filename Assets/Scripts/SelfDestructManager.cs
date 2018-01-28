using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void GlobalEvent();

public class SelfDestructManager : MonoBehaviour
{
    //Basically Instance :D
    public static SelfDestructManager InstantKiller { get; private set; }

    public event GlobalEvent onSelfDestruct;

    public Color alertAmbientColor;

    internal float timeLeft = 194;

    private bool isInitiated = false;


    void Awake()
    {
        InstantKiller = this;
    }

    void Update()
    {
        if(isInitiated)
        {
            if(timeLeft - Time.deltaTime <= 0)
            {
                MazeLevelManager.Instance.vrPlayer.Die();
                enabled = false;
            }
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, float.MaxValue);
        }
    }

    public string GetTimeText()
    {
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            minutes = 0;
            seconds = 0;
        }
        return "00:" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void InitiateSelfDestruct()
    {
        if (!isInitiated)
        {
            onSelfDestruct.Invoke();
            isInitiated = true;
            RenderSettings.ambientSkyColor = alertAmbientColor;
            RenderSettings.fogColor = alertAmbientColor;
            MazeLevelManager.Instance.FadeOutMusic();
            GetComponent<AudioSource>().Play();
        }
    }
}
