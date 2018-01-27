using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    public static AIManager Instance { get; private set; }

    public GameObject[] WayPoints;

    // Use this for initialization
    void Awake() {
        Instance = this;
	}
	

}
