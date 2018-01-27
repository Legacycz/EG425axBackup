using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePuzzle : MonoBehaviour {
    public List<RuneButton> runes = new List<RuneButton>();
    public int solutionSize;
    public List<RuneButton> solution = new List<RuneButton>();
    public List<bool> solutionAnswers = new List<bool>();

    private int currentAnswerIndex = 0;

    private void Start()
    {
        for(int i=0; i<runes.Count;++i)
        {
            runes[i].puzzleRoot = this;
        }

        for(int i=0; i<solutionSize; ++i)
        {
            solution.Add(runes[UnityEngine.Random.Range(0, runes.Count)]);
            solutionAnswers.Add(false);
        }
    }

    internal void CheckAnswer(RuneButton pressedRuneButton)
    {
        if (pressedRuneButton == runes[currentAnswerIndex])
        {
            solutionAnswers[currentAnswerIndex] = true;
            ++currentAnswerIndex;
            if(currentAnswerIndex >= solutionSize)
            {
                ResolveSolution();
            }
        }
        else
        {
            print("wrong answer");
            for (int i = 0; i < solutionSize; ++i)
            {
                solutionAnswers[i] = false;
            }
            currentAnswerIndex = 0;
        }
    }

    public void ResolveSolution()
    {
        print("good job");
    }
}
