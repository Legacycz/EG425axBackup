using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour {
    public MeshRenderer thisMeshRenderer;
    public Collider thisCollider;

    public void ToggleLaserArea()
    {
        thisMeshRenderer.enabled = !thisMeshRenderer.enabled;
        thisCollider.enabled = thisMeshRenderer.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        switch(other.tag)
        {
            case "Player":
                MazeLevelManager.Instance.vrPlayer.Die();
                break;
            case "Enemy":
                other.GetComponent<AIBase>().Die();
                break;
        }
    }
}
