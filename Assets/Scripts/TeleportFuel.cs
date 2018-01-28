using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportFuel : MonoBehaviour {
    public VRTK_DashTeleport thisTeleport;
    public float requiredTeleportFuel = 0.025f;
    public float fuelUseMultiplier = 0.25f;

	// Use this for initialization
	void Start () {
        thisTeleport.Teleported += ThisTeleport_Teleported;
	}

    private void ThisTeleport_Teleported(object sender, DestinationMarkerEventArgs e)
    {
        print("Remove fuel");
        MazeLevelManager.Instance.vrPlayer.RemoveFuel(requiredTeleportFuel);
    }

    // Update is called once per frame
    void Update () {
		if(MazeLevelManager.Instance.vrPlayer)
        {
            MazeLevelManager.Instance.vrPlayer.RemoveFuel(Time.deltaTime * fuelUseMultiplier);
        }

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            MazeLevelManager.Instance.vrPlayer.RemoveFuel(-requiredTeleportFuel);
        }
	}
}
