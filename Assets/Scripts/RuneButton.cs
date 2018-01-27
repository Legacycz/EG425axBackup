using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneButton : InteractiveObject {

    internal RunePuzzle puzzleRoot;

    private void Start()
    {
        spriteRenderer.sprite = MazeLevelManager.Instance.abilityIconsAtlas[Random.Range(0, MazeLevelManager.Instance.abilityIconsAtlas.Length)];
    }

    public override void ObjectUsed()
    {
        puzzleRoot.CheckAnswer(this);
    }
}
