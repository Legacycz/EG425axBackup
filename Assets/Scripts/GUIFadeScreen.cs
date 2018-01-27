using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFadeScreen : MonoBehaviour {
    public CanvasGroup thisCanvasGroup;

    public void StartUIScreenFadeIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(ScreenUIFadeInSequence());
    }

    private IEnumerator ScreenUIFadeInSequence()
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

    public void StartUIScreenFadeOut()
    {
        StartCoroutine(ScreenUIFadeOutSequence());
    }

    private IEnumerator ScreenUIFadeOutSequence()
    {
        float elapsedTime = 0;
        thisCanvasGroup.alpha = 1;

        while (elapsedTime < 1)
        {
            thisCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime);
            elapsedTime += Time.deltaTime / 3;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
