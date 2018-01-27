using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    private VRTK_HeadsetFade headsetFade;
    private bool isDying = false;

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
        if (!isDying)
        {
            headsetFade.Fade(Color.red, 2f);
            StartCoroutine(DieSequence());
        }
    }

    private IEnumerator DieSequence()
    {
        isDying = true;
        print("Explosion sprite on operator map.");
        yield return new WaitForSeconds(3);
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.position = MazeLevelManager.Instance.gameOverPoint.position;
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.rotation = Quaternion.identity;
        headsetFade.Fade(Color.clear, 2f);
        yield return new WaitForSeconds(2);
        isDying = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Block")
        {
            print("Fade initialized");
            MazeLevelManager.Instance.activeBlock = col.gameObject.GetComponent<MazeBlock>();
            MazeLevelManager.Instance.activeBlock.RevealBlock();
        }
    }
}