using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MazeBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<PlayerVisibility>())
            {
                print("Fade in");
                VRTK_ScreenFade.Start(Color.clear, 1f);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<PlayerVisibility>())
            {
                print("Fade out");
                VRTK_ScreenFade.Start(Color.black, 1f);
            }
        }
    }
}
