using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    private bool isInitialized = false;

    // Use this for initialization
    private void OnTriggerEnter(Collider col)
    {
        if (!isInitialized)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("VisibleArea"))
            {
                print("Fade initialized");
                isInitialized = true;
                MazeLevelManager.Instance.activeBlock = col.gameObject.GetComponent<MazeBlock>();
            }
        }
    }
}