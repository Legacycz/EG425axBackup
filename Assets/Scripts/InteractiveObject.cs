using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InteractiveObject : MonoBehaviour {
    public VRTK_InteractableObject thisInteractableObject;
    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void OnEnable () {
        thisInteractableObject.InteractableObjectUsed += ThisInteractableObject_InteractableObjectUsed;
	}

    private void ThisInteractableObject_InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        ObjectUsed();
    }

    [ContextMenu("ForceObjectUsed")]
    public virtual void ObjectUsed()
    {
        print("Object used.");
    }
}
