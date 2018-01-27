using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngameToggleButton : InteractiveObject {
    public UnityEvent OnToggleUsed;
    public bool isDebugOnly = false;

    void Start()
    {
        if(isDebugOnly && !Application.isEditor)
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("ForceObjectUsedDerived")]
    public override void ObjectUsed()
    {
        OnToggleUsed.Invoke();
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }
}
