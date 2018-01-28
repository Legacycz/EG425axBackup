using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class MazeBlock : MonoBehaviour
{
    public GameObject blockToReveal;

    public MazeBlock rightBlock;
    public MazeBlock leftBlock;
    public MazeBlock upBlock;
    public MazeBlock downBlock;
    public UnityEvent OnEnterToBlock;

    private void OnEnable()
    {
        RaycastHit rayHit;

        Ray checkRight = new Ray(transform.position, transform.right);
        if (Physics.Raycast(checkRight, out rayHit, 4f))
        {
            if (rayHit.transform.tag == "Block")
            {
                rightBlock = rayHit.transform.GetComponent<MazeBlock>();
            }
        }

        Ray checkLeft = new Ray(transform.position, -transform.right);
        if (Physics.Raycast(checkLeft, out rayHit, 4f))
        {
            if (rayHit.transform.tag == "Block")
            {
                leftBlock = rayHit.transform.GetComponent<MazeBlock>();
            }
        }

        Ray checkUp = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(checkUp, out rayHit, 4f))
        {
            if (rayHit.transform.tag == "Block")
            {
                upBlock = rayHit.transform.GetComponent<MazeBlock>();
            }
        }

        Ray checkDown = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(checkDown, out rayHit, 4f))
        {
            if (rayHit.transform.tag == "Block")
            {
                downBlock = rayHit.transform.GetComponent<MazeBlock>();
            }
        }

        StartCoroutine(DelayedOnEnable());
    }

    private IEnumerator DelayedOnEnable()
    {
        yield return null;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<VRPlayer>())
            {
                RevealBlock();
                if(OnEnterToBlock != null)
                {
                    OnEnterToBlock.Invoke();
                }
                //VRTK_ScreenFade.Start(Color.clear, 1f);
            }
        }
    }

    public void RevealBlock()
    {
        //print("Reveal block");
        blockToReveal.SetActive(false);

        if (rightBlock)
        {
            rightBlock.blockToReveal.SetActive(false);
        }
        if (leftBlock)
        {
            leftBlock.blockToReveal.SetActive(false);
        }
        if (upBlock)
        {
            upBlock.blockToReveal.SetActive(false);
        }
        if (downBlock)
        {
            downBlock.blockToReveal.SetActive(false);
        }
    }

    /*private void OnTriggerExit(Collider col)
    {
        if (MazeLevelManager.Instance.activeBlock == this)
        {
            if (col.gameObject.GetComponent<VRPlayer>())
            {
                print("Fade out");
                //VRTK_ScreenFade.Start(Color.black, 1f);
            }
        }
    }*/
}
