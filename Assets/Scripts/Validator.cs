using UnityEngine;
using System.Collections;

public class Validator : SingletonBehaviour<Validator>
{
    [SerializeField, ReadOnly]
    Tile goalTile = null;
    [SerializeField]
    GameObject winObject = null;
    [SerializeField]
    GameObject failObject = null;
    [SerializeField]
    float winWaitTime = 0.0f;
    [SerializeField]
    float failWaitTime = 0.0f;

    [ReadOnly]
    State state = State.Idle;

    Coroutine coroutine = null;

    enum State
    {
        None = 0,
        Idle,
        Success,
        Fail,
        Dissent,
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void SetGoalTile( Tile tile )
    {
        if (instance.goalTile != null)
        {
            Debug.LogError("Goal tile already set.");
        }

        instance.goalTile = tile;
    }

    public void Validate()
    {
        if (instance.goalTile == null)
        {
            Debug.LogError("Goal tile null");
            return;
        }

        if (state == State.Dissent)
        {
            return;
        }

        // recursively validates the tree
        if (goalTile.IsValid())
        {
            ShowWinEffect();
        }
        else
        {
            ShowFailEffect();
        }
    }

    void ShowWinEffect()
    {
        switch (state)
        {
            case State.Idle: break;
            case State.Success: return; break;
            case State.Fail: CleanUpFailEffect(); break;
            default: Debug.LogError("Unexpected state"); break;
        }

        state = State.Success;
        winObject.SetActive(true);
        TileSpawner.instance.DisableAllTiles();
        coroutine = StartCoroutine(FinishLevelRoutine());
    }

    void ShowFailEffect()
    {
        switch (state)
        {
            case State.Idle: break;
            case State.Success: return; break;
            case State.Fail: return; break;
            default: Debug.LogError("Unexpected state"); break;
        }

        state = State.Fail;
        failObject.SetActive(true);
        coroutine = StartCoroutine(TurnOffFailEffectRoutine());
    }

    void CleanUpWinEffect()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        winObject.SetActive(false);
    }

    void CleanUpFailEffect()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        failObject.SetActive(false);
    }

    IEnumerator TurnOffFailEffectRoutine()
    {
        yield return new WaitForSeconds(failWaitTime);
        failObject.SetActive(false);
        state = State.Idle;
    }

    IEnumerator FinishLevelRoutine()
    {
        yield return new WaitForSeconds(winWaitTime);
        winObject.SetActive(false);
        state = State.Dissent;

        ThoughtBubbleSpawner.instance.Spawn();
    }
}
