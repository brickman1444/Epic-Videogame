using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstThoughtBubbleSpawner : ThoughtBubbleSpawner
{
    [SerializeField]
    GameObject Instructions = null;

    public override void Spawn()
    {
        Debug.Log("first thought bubble spawner");
        PlaceYesBubbles();
        Instructions.SetActive(true);
        MakeNewNo();
    }
}
