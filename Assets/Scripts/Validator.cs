using UnityEngine;
using System.Collections;

public class Validator : SingletonBehaviour<Validator>
{
    [SerializeField, ReadOnly]
    Tile goalTile = null;

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

        Tile currentTile = goalTile;

        while (currentTile != null)
        {
            if (currentTile.isStartTile)
            {
                Debug.Log("Valid solution.");
                return;
            }

            currentTile = currentTile.parent;
        }

        Debug.Log("Invalid solution");
    }
}
