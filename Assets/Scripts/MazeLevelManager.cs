using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MazeLevelManager : MonoBehaviour {
    public static MazeLevelManager Instance { get; private set; }

    public GameObject playerVisualization;
    public MazeBlock activeBlock;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        VRTK_SDKManager.instance.LoadedSetupChanged += Instance_LoadedSetupChanged;
	}

    private void Instance_LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        if (e.currentSetup)
        {
            playerVisualization.transform.SetParent(e.currentSetup.actualHeadset.transform);
            playerVisualization.transform.localPosition = Vector3.zero;
            playerVisualization.transform.localRotation = Quaternion.identity;
        }
    }
}
