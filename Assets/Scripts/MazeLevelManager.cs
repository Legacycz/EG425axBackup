using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MazeLevelManager : MonoBehaviour {
    public static MazeLevelManager Instance { get; private set; }

    public MazeBlock activeBlock;
    public Transform gameOverPoint;

    internal VRPlayer vrPlayer;

    public Sprite[] abilityIconsAtlas;

    public UsableDisplay Usable;

    private void Awake()
    {
        Instance = this;
        abilityIconsAtlas = Resources.LoadAll<Sprite>("Symbols");
    }

    // Use this for initialization
    void Start ()
    {
        VRTK_SDKManager.instance.LoadedSetupChanged += Instance_LoadedSetupChanged;
	}

    private void Instance_LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        if (e.currentSetup && vrPlayer)
        {
            vrPlayer.transform.SetParent(e.currentSetup.actualHeadset.transform);
            vrPlayer.transform.localPosition = Vector3.zero;
            vrPlayer.transform.localRotation = Quaternion.identity;
        }
    }

    [ContextMenu("RevealMap")]
    public void RevealMap()
    {
        if (Application.isPlaying)
        {
            MazeBlock[] tiles = FindObjectsOfType<MazeBlock>();
            for (int i = 0; i < tiles.Length; ++i)
            {
                tiles[i].RevealBlock();
            }
        }
    }
}
