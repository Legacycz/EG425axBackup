using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRPlayer : MonoBehaviour
{
    public GameObject billboardObject;
    public GameObject iconObject;
    public Color playerColor;
    public float fuel = 100;

    private VRTK_HeadsetFade headsetFade;
    private bool isDeathBlocked = false;

    private bool isDead = false;

    void Awake()
    {
        billboardObject.GetComponent<SpriteRenderer>().color = playerColor;
        iconObject.GetComponent<SpriteRenderer>().color = playerColor;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(DelayedOnEnable());
    }

    protected virtual void OnDisable()
    {
        billboardObject.SetActive(true);
    }

    private IEnumerator DelayedOnEnable()
    {
        yield return null;
        EnablePlayer();
    }

    private void EnablePlayer()
    {
        billboardObject.SetActive(false);
        GetComponent<AudioSource>().Play();
        headsetFade = (headsetFade != null ? headsetFade : FindObjectOfType<VRTK_HeadsetFade>());
        if (MazeLevelManager.Instance.vrPlayer && MazeLevelManager.Instance.vrPlayer != this)
        {
            MazeLevelManager.Instance.vrPlayer.DisablePlayer();
        }
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.position = transform.position;
        //VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.rotation = transform.rotation;
        transform.SetParent(VRTK_SDKManager.instance.loadedSetup.actualHeadset.transform);
        iconObject.transform.SetParent(transform);
        iconObject.transform.localPosition = Vector3.zero;
        iconObject.transform.localEulerAngles = new Vector3(90, 90, 0);        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        RemoveFuel(0);
        MazeLevelManager.Instance.vrPlayer = this;

    }

    public void DisablePlayer()
    {
        transform.SetParent(null);
        iconObject.transform.SetParent(null);
        iconObject.transform.localEulerAngles = new Vector3(90, 90, 0);
        enabled = false;
        PlayerIcon scr = iconObject.GetComponent<PlayerIcon>();
        if (scr)
        {
            scr.SetReadyToActivate();
        }
    }

    public void RemoveFuel(float fuelAmount)
    {
        fuel -= fuelAmount;
        MazeLevelManager.Instance.fuelbar.fillAmount = fuel / 100f;

        if (fuel <= 0)
        {
            Die();
        }
    }

    [ContextMenu("Win")]
    public void Win()
    {
        if(!isDeathBlocked)
        {
            headsetFade.Fade(Color.green, 2f);
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        isDeathBlocked = true;
        MazeLevelManager.Instance.winGameScreen.StartUIScreenFadeIn();
        yield return new WaitForSeconds(3);
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.position = MazeLevelManager.Instance.gameOverPoint.position;
        VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.rotation = Quaternion.identity;
        headsetFade.Fade(Color.white, 2f);
        yield return new WaitForSeconds(2);
        //isDeathBlocked = false;
    }

    [ContextMenu("Die")]
    public void Die()
    {
        if (!isDeathBlocked && !isDead)
        {
            headsetFade.Fade(Color.red, 2f);
            headsetFade.GetComponent<VRTK_HeadsetCollisionFade>().enabled = false;
            MazeLevelManager.Instance.layoutImage.raycastTarget = false;
            VRTK_SDKManager.instance.scriptAliasLeftController.GetComponent<TeleportBlocker>().BlockController();
            VRTK_SDKManager.instance.scriptAliasRightController.GetComponent<TeleportBlocker>().BlockController();
            StartCoroutine(DieSequence());
        }
    }

    private IEnumerator DieSequence()
    {
        isDeathBlocked = true;
        isDead = true;
        MazeLevelManager.Instance.gameOverScreen.StartUIScreenFadeIn();
        yield return new WaitForSeconds(3);
        //VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.position = MazeLevelManager.Instance.gameOverPoint.position;
        //VRTK_SDKManager.instance.loadedSetup.actualBoundaries.transform.rotation = Quaternion.identity;
        headsetFade.Fade(Color.black, 2f);
        yield return new WaitForSeconds(2);
        //isDeathBlocked = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Block" && enabled)
        {
            MazeLevelManager.Instance.activeBlock = col.gameObject.GetComponent<MazeBlock>();
            MazeLevelManager.Instance.activeBlock.RevealBlock();
        }
    }
}