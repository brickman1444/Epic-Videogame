using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
        if (!Application.isEditor)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
