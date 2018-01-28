using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArea : MonoBehaviour {
    public MeshRenderer thisMeshRenderer;
    public Collider thisCollider;
    public AudioSource thisAudioSource;

    public void ToggleLaserArea()
    {
        thisMeshRenderer.enabled = !thisMeshRenderer.enabled;
        thisCollider.enabled = thisMeshRenderer.enabled;
        if (thisMeshRenderer.enabled)
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
                other.GetComponent<AIBase>().Die();
                break;
        }
    }
}
