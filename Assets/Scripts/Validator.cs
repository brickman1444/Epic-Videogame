using UnityEngine;
using System.Collections;

public class Validator : SingletonBehaviour<Validator>
{
    [SerializeField, ReadOnly]
    Tile goalTile = null;
    [SerializeField]
    string nextLevelName = "";
    [SerializeField]
    GameObject winObject = null;
    [SerializeField]
    GameObject failObject = null;
    [SerializeField]
    float winWaitTime = 0.0f;
    [SerializeField]
    float failWaitTime = 0.0f;

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
        winObject.SetActive(true);
        StartCoroutine(FinishLevelRoutine());
    }

    void ShowFailEffect()
    {
        failObject.SetActive(true);
        StartCoroutine(TurnOffFailEffectRoutine());
    }

    IEnumerator TurnOffFailEffectRoutine()
    {
        yield return new WaitForSeconds(failWaitTime);
        failObject.SetActive(false);
    }

    IEnumerator FinishLevelRoutine()
    {
        yield return new WaitForSeconds(winWaitTime);
        winObject.SetActive(false);

        ThoughtBubbleSpawner.instance.Spawn();

        //if (nextLevelName.Length != 0)
        //{
        //    Application.LoadLevel(nextLevelName);
        //}
    }
}
