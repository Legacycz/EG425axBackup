using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    private bool isInitialized = false;
    private VRTK_HeadsetFade headsetFade;

    public void Start()
    {
        MazeLevelManager.Instance.vrPlayer = this;
    }

    protected virtual void OnEnable()
    {
        headsetFade = (headsetFade != null ? headsetFade : FindObjectOfType<VRTK_HeadsetFade>());
    }

    [ContextMenu("Die")]
    public void Die()
    {
        headsetFade.Fade(Color.red, 2f);
        StartCoroutine(DieSequence());
    }

    private IEnumerator DieSequence()
    {
        print("Explosion sprite on operator map.");
        yield return new WaitForSeconds(3);
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.position = MazeLevelManager.Instance.gameOverPoint.position;
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.rotation = Quaternion.identity;
        headsetFade.Fade(Color.clear, 2f);
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (!isInitialized)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("VisibleArea"))
            {
                print("Fade initialized");
                isInitialized = true;
                MazeLevelManager.Instance.activeBlock = col.gameObject.GetComponent<MazeBlock>();
                MazeLevelManager.Instance.activeBlock.RevealBlock();
            }
        }
    }
}