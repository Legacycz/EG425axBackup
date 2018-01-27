using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MazeLevelManager : MonoBehaviour {
    public static MazeLevelManager Instance { get; private set; }
    
    public MazeBlock activeBlock;
    public Transform gameOverPoint;

    internal VRPlayer vrPlayer;

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
            vrPlayer.transform.SetParent(e.currentSetup.actualHeadset.transform);
            vrPlayer.transform.localPosition = Vector3.zero;
            vrPlayer.transform.localRotation = Quaternion.identity;
        }
    }
}
