using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFadeScreen : MonoBehaviour {
    public CanvasGroup thisCanvasGroup;

    public void StartScreenFade()
    {
        gameObject.SetActive(true);
        StartCoroutine(ScreenFadeSequence());
    }

    private IEnumerator ScreenFadeSequence()
    {
        float elapsedTime = 0;
        thisCanvasGroup.alpha = 0;

        while (elapsedTime < 1)
        {
            thisCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime);
            elapsedTime += Time.deltaTime / 3;
            yield return null;
        }
    }
}
