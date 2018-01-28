using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour {
    public MeshRenderer thisMeshRenderer;
    public GameObject LasersVisual;
    public Collider thisCollider;

    public void ToggleLaserArea()
    {
        LasersVisual.SetActive(!LasersVisual.activeInHierarchy);
        thisCollider.enabled = LasersVisual.activeInHierarchy;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.isTrigger)
            return;
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
