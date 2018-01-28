using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour {
    //public MeshRenderer thisMeshRenderer;
    public GameObject LasersVisual;
    public Collider thisCollider;
    public AudioSource thisAudioSource;

    public void ToggleLaserArea()
    {
        LasersVisual.SetActive(!LasersVisual.activeInHierarchy);
        thisCollider.enabled = LasersVisual.activeInHierarchy;
        if(LasersVisual.activeInHierarchy)
        {
            thisAudioSource.Play();
        }
        else
        {
            thisAudioSource.Stop();
        }
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
                if (other.isTrigger)
                    break;
                other.GetComponent<AIBase>().Die();
                break;
        }
    }
}
