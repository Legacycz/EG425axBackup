using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MazeBlock : MonoBehaviour
{
    public GameObject blockToReveal;

    private void OnTriggerEnter(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<VRPlayer>())
            {
                RevealBlock();
                //VRTK_ScreenFade.Start(Color.clear, 1f);
            }
        }
    }

    public void RevealBlock()
    {
        print("Reveal block");
        blockToReveal.SetActive(false);
    }

    /*private void OnTriggerExit(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<VRPlayer>())
            {
                print("Fade out");
                //VRTK_ScreenFade.Start(Color.black, 1f);
            }
        }
    }*/
}
