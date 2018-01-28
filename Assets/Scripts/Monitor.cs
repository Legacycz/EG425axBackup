using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour {
    public List<GameObject> slides = new List<GameObject>();
    public GameObject overlayImage;

    public GameObject countdownSlide;
    public Text countdownText;

    private int slideDuration = 10;
    private int currentSlide;


	// Use this for initialization
	void Start () {
        SelfDestructManager.InstantKiller.onSelfDestruct += InstantKiller_onSelfDestruct;
        currentSlide = Random.Range(0, slides.Count);
        StartCoroutine(Slideshow());
	}

    private void InstantKiller_onSelfDestruct()
    {
        StopAllCoroutines(); 
        slides[currentSlide].SetActive(false);
        countdownSlide.SetActive(true);
        Color newColor = SelfDestructManager.InstantKiller.alertAmbientColor;
        overlayImage.GetComponent<Image>().color = newColor;
        newColor.a = 1;
        GetComponent<SpriteRenderer>().color = newColor;
    }

    private void LateUpdate()
    {
        countdownText.text = SelfDestructManager.InstantKiller.GetTimeText();
    }

    private IEnumerator Slideshow()
    {
        while (true)
        {
            if (slides.Count > 0)
            {
                slides[currentSlide].SetActive(false);
                currentSlide = (currentSlide + 1) % slides.Count;
                slides[currentSlide].SetActive(true);
                slideDuration = Random.Range(10, 20);
            }
            yield return new WaitForSeconds(slideDuration);
        }
    }
}
