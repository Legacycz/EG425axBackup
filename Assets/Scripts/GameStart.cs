using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
    public GUIFadeScreen startFadeGUI;
    public float hideDelay = 20;

    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(hideDelay);
        startFadeGUI.StartUIScreenFadeOut();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
