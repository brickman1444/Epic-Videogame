using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalThoughtBubbleSpawner : ThoughtBubbleSpawner
{
    [SerializeField]
    float timeToWaitBeforeEnd = 0.0f;

    public override void Spawn()
    {
        PlaceYesBubbles();
        StartCoroutine(WaitThenCloseRoutine());
    }

    IEnumerator WaitThenCloseRoutine()
    {
        yield return new WaitForSeconds(timeToWaitBeforeEnd);
        Debug.Log("Quit");
        Application.Quit();
    }
}
