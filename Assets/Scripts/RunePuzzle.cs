using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunePuzzle : MonoBehaviour
{
    public UnityEvent OnResolved;

    public List<RuneButton> runes = new List<RuneButton>();
    public int solutionSize;
    public List<RuneButton> solution = new List<RuneButton>();
    public List<bool> solutionAnswers = new List<bool>();
    public AudioSource thisAudioSource;
    public AudioClip correctSoundFX, wrongSoundFX;

    private int currentAnswerIndex = 0;
    private bool isSolved = false;

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

        if (pressedRuneButton == runes[currentAnswerIndex])
        {
            solutionAnswers[currentAnswerIndex] = true;
            ++currentAnswerIndex;
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
            for (int i = 0; i < solutionSize; ++i)
            {
                solutionAnswers[i] = false;
            }
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
