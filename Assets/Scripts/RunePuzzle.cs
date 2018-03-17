using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RunePuzzle : MonoBehaviour
{
    public UnityEvent OnResolved;

    public List<RuneButton> runes = new List<RuneButton>();
    public int solutionSize;
    public List<RuneButton> solution = new List<RuneButton>();
    public List<bool> solutionAnswers = new List<bool>();
    public List<SpriteRenderer> strikeIcons = new List<SpriteRenderer>();
    public AudioSource thisAudioSource;
    public AudioClip correctSoundFX, wrongSoundFX;
    public int allowedStrikes = 2;
    public Text monitorText;

    internal bool isSolved = false;
    internal List<Sprite> usedSprites = new List<Sprite>();

    private int currentAnswerIndex = 0;
    private int currentStrikes = 0;

    private void Awake()
    {
        for (int i = 0; i < runes.Count; ++i)
        {
            runes[i].puzzleRoot = this;
        }
    }

    private void Start()
    {
        for(int i=0; i<solutionSize; ++i)
        {
            solution.Add(runes[UnityEngine.Random.Range(0, runes.Count)]);
            solutionAnswers.Add(false);
        }
    }

    internal void CheckAnswer(RuneButton pressedRuneButton)
    {
        if(isSolved)
        {
            return;
        }

        if (pressedRuneButton == solution[currentAnswerIndex])
        {
            solutionAnswers[currentAnswerIndex] = true;
            ++currentAnswerIndex;
            monitorText.text = "--- Lock Interface ---\nPlease enter the sequence to open the door.\n" + currentAnswerIndex + "/ 4";
            if (thisAudioSource)
            {
                thisAudioSource.PlayOneShot(correctSoundFX);
            }
            if (currentAnswerIndex >= solutionSize)
            {
                ResolvePuzzle();
            }
        }
        else
        {
            if (thisAudioSource)
            {
                thisAudioSource.PlayOneShot(wrongSoundFX);
            }

            print("wrong answer");
            if (currentStrikes > allowedStrikes)
            {
                SelfDestructManager.InstantKiller.InitiateSelfDestruct();
                ResolvePuzzle();
            }
            else
            {
                strikeIcons[currentStrikes].color = Color.red;
                ++currentStrikes;
            }
            for (int i = 0; i < solutionSize; ++i)
            {
                solutionAnswers[i] = false;
            }
            monitorText.text = "--- Lock Interface ---\nPlease enter the sequence to open the door.\n0 / 4";
            currentAnswerIndex = 0;
        }
    }

    public void ResolvePuzzle()
    {
        OnResolved.Invoke();
        print("solved");
        isSolved = true;
    }
}
