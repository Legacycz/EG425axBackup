using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneButton : InteractiveObject {

    public List<int> index;
    internal RunePuzzle puzzleRoot;

    private void Start()
    {
        Sprite newSprite;
        do
        {
            newSprite = MazeLevelManager.Instance.abilityIconsAtlas[Random.Range(0, MazeLevelManager.Instance.abilityIconsAtlas.Length)];
        }
        while (puzzleRoot.usedSprites.Contains(newSprite));

        puzzleRoot.usedSprites.Add(newSprite);
        spriteRenderer.sprite = newSprite;
    }

    [ContextMenu("ForceObjectUsedDerived")]
    public override void ObjectUsed()
    {
        puzzleRoot.CheckAnswer(this);
    }
}
