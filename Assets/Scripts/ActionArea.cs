using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionArea : MonoBehaviour
{
    public UnityEvent OnBlockStepped;
    public AudioSource thisAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);

        if (other.tag == "Player")
        {
            OnBlockStepped.Invoke();
        }
    }
}
