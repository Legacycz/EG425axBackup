using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRTK;

public class MazeLevelManager : MonoBehaviour {
    public static MazeLevelManager Instance { get; private set; }

    public MazeBlock activeBlock;
    public Transform gameOverPoint;
    public GUIFadeScreen gameOverScreen;
    public GUIFadeScreen winGameScreen;
    public Image fuelbar;
    public AudioSource thisAudioSource;
    public Image layoutImage;
    public RuneSolution runePuzzleSolutionGUI;

    internal VRPlayer vrPlayer;
    
    public Sprite[] abilityIconsAtlas;

    public UsableDisplay Usable;

    private void Awake()
    {
        Instance = this;
        abilityIconsAtlas = Resources.LoadAll<Sprite>("Symbols");
    }

    // Use this for initialization
    void Start ()
    {
        VRTK_SDKManager.instance.LoadedSetupChanged += Instance_LoadedSetupChanged;
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Instance_LoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
        if (e.currentSetup && vrPlayer)
        {
            vrPlayer.transform.SetParent(e.currentSetup.actualHeadset.transform);
            vrPlayer.transform.localPosition = Vector3.zero;
            vrPlayer.transform.localRotation = Quaternion.identity;
        }
    }

    public void WinGame()
    {
        vrPlayer.Win();
    }

    [ContextMenu("RevealMap")]
    public void RevealMap()
    {
        if (Application.isPlaying)
        {
            MazeBlock[] tiles = FindObjectsOfType<MazeBlock>();
            for (int i = 0; i < tiles.Length; ++i)
            {
                tiles[i].RevealBlock();
            }
        }
    }

    public void FadeOutMusic()
    {
        StopCoroutine(FadeOutMusicSequence());
        StartCoroutine(FadeOutMusicSequence());
    }

    private IEnumerator FadeOutMusicSequence()
    {
        float elapsedTime = 0;
        float startVolume = thisAudioSource.volume;

        while (elapsedTime < 1)
        {
            thisAudioSource.volume = Mathf.Lerp(1, 0, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        thisAudioSource.Stop();
    }
}
