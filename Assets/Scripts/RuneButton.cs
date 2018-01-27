using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneButton : InteractiveObject {
    internal RunePuzzle puzzleRoot;

    private void Start()
    {
        // set random rune image
    }

    public override void ObjectUsed()
    {
        puzzleRoot.CheckAnswer(this);
    }
}
