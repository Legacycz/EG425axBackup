using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorCameraController : MonoBehaviour {

    public Camera Camera;
    public float ZoomOutMaxSize = 20;
    public float ZoomOnPlayerSize = 10;
    public bool SmoothFollowPlayer = false;
    public Vector3 DefaultPosition;
    public float PostitoinDumping = 2;
    public float HieghtplayerFollow = 10; 


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(SmoothFollowPlayer)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, MazeLevelManager.Instance.vrPlayer.transform.position + Vector3.up * HieghtplayerFollow, PostitoinDumping * Time.deltaTime);
        }
		
	}

    [ContextMenu("Follow")]
    public void ZoomFollowPlayer()
    {
        Camera.orthographicSize = ZoomOnPlayerSize;
        Camera.transform.position = MazeLevelManager.Instance.vrPlayer.transform.position + Vector3.up * HieghtplayerFollow;
        SmoothFollowPlayer = true;
    }

    [ContextMenu("Default")]
    public void ZoomOut()
    {
        Camera.orthographicSize = ZoomOutMaxSize;
        Camera.transform.position = DefaultPosition;
        SmoothFollowPlayer = false;
    }


}
